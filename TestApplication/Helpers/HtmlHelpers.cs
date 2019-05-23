using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace TestApplication.Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString PartialFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string partialViewName)
        {
            string name = ExpressionHelper.GetExpressionText(expression);
            object model = ModelMetadata.FromLambdaExpression(expression, helper.ViewData).Model;
            StringBuilder htmlFieldPrefix = new StringBuilder();
            if (helper.ViewData.TemplateInfo.HtmlFieldPrefix != "")
            {
                htmlFieldPrefix.Append(helper.ViewData.TemplateInfo.HtmlFieldPrefix);
                htmlFieldPrefix.Append(name == "" ? "" : "." + name);
            }
            else
                htmlFieldPrefix.Append(name);

            var viewData = new ViewDataDictionary(helper.ViewData)
            {
                TemplateInfo = new TemplateInfo
                {
                    HtmlFieldPrefix = htmlFieldPrefix.ToString()
                }
            };

            return helper.Partial(partialViewName, model, viewData);
        }
    }
}