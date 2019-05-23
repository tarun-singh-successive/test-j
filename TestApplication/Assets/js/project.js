var project = new function () {
    var instance = this;

    this.loadStates = function (incorpCountry) {
        var url = common.mapUrl('Company', 'GetStatesByCountryId');
        $.get(common.mapUrl('Company', 'GetStatesByCountryId'), { 'countryId': incorpCountry.value }, function (response) {
            common.unblockElement($sender.parent());
            $sender.parent().append(response);
            $sender.toggleClass('fa-minus-square');
            $sender.toggleClass('fa-plus-square');
            $sender.attr('onClick', 'service.closeSalonServices(this)');
            $sender.attr('title', 'Collapse');
        });
    }
}