Create table CompanyProfile
(
Id int Identity(1,1) Primary Key not null,
CompanyName varchar(200),
ShortName varchar (100),
AboutCompany varchar(max),
CIN varchar(200),
PAN varchar(200),
TAN varchar(200),
CompanyCategoryId int not null,
COmpanyClass varchar(200),
AuthorizedCapital decimal(18,4) not null,
PaidupCapital decimal(18,4)not null,
ShareNominalValue decimal(18,4)not null,
StateOfRegistration varchar(100),
IncorporationCountryId varchar(100),
IncorporationStateId int not null,
IncorporationDate DateTime not null,
Email varchar(100),
Mobile varchar(15),
Landline varchar(20),
Address1 varchar(max),
Address2 varchar(max),
City varchar(100),
StateId int not null,
Pincode int not null,
CountryId int not null,
)

drop table CompanyProfile

alter table CompanyProfile alter column IncorporationCountryId int not null

alter table CompanyProfile add constraint Fk_CompanyProfile_Countries_IncorporationCountryId Foreign Key (IncorporationCountryId) references Countries(Id)