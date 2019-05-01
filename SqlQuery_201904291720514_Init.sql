﻿DROP PROCEDURE IF EXISTS `_idempotent_script`;

DELIMITER //

CREATE PROCEDURE `_idempotent_script`()
BEGIN
  DECLARE CurrentMigration TEXT;

  IF EXISTS(SELECT 1 FROM information_schema.tables 
  WHERE table_name = '__MigrationHistory' 
  AND table_schema = DATABASE()) THEN 
    SET CurrentMigration = (SELECT
`Project1`.`MigrationId`
FROM `__MigrationHistory` AS `Project1`
 WHERE `Project1`.`ContextKey` = 'RDB.Data.Migrations.Configuration'
 ORDER BY 
`Project1`.`MigrationId` DESC LIMIT 1);
  END IF;

  IF CurrentMigration IS NULL THEN
    SET CurrentMigration = '0';
  END IF;

  IF CurrentMigration < '201904291720514_Init' THEN 
create table `Autobus` (`spz` nvarchar(50)  not null collate utf8_bin ,`znacka` nvarchar(50)  not null collate utf8_bin ,primary key ( `spz`) ) engine=InnoDb auto_increment=0;
CREATE index  `IX_znacka` on `Autobus` (`znacka` DESC) using HASH;
create table `Znacka` (`znacka` nvarchar(50)  not null collate utf8_bin ,primary key ( `znacka`) ) engine=InnoDb auto_increment=0;
create table `Klient` (`email` nvarchar(50)  not null collate utf8_bin ,`jmeno` nvarchar(50)  not null ,`prijmeni` nvarchar(50)  not null ,primary key ( `email`) ) engine=InnoDb auto_increment=0;
create table `Kontakt` (`hodnota` nvarchar(50)  not null collate utf8_bin ,`typ` nvarchar(50)  not null ,`cislo_rp` nvarchar(50)  not null ,primary key ( `hodnota`) ) engine=InnoDb auto_increment=0;
CREATE index  `IX_typ` on `Kontakt` (`typ` DESC) using HASH;
CREATE index  `IX_cislo_rp` on `Kontakt` (`cislo_rp` DESC) using HASH;
create table `TypKontaktu` (`typ` nvarchar(50)  not null collate utf8_bin ,primary key ( `typ`) ) engine=InnoDb auto_increment=0;
create table `Ridic` (`cislo_rp` nvarchar(50)  not null collate utf8_bin ,`jmeno` nvarchar(50)  not null ,`prijmeni` nvarchar(50)  not null ,primary key ( `cislo_rp`) ) engine=InnoDb auto_increment=0;
create table `Jizda` (`linka` nvarchar(50)  not null collate utf8_bin ,`spz` nvarchar(50)  not null ,`cislo_rp` nvarchar(50)  not null ,`cas` timestamp(5) not null ,primary key ( `cas`,`linka`) ) engine=InnoDb auto_increment=0;
CREATE index  `IX_linka` on `Jizda` (`linka` DESC) using HASH;
CREATE index  `IX_spz` on `Jizda` (`spz` DESC) using HASH;
CREATE index  `IX_cislo_rp` on `Jizda` (`cislo_rp` DESC) using HASH;
create table `Trasy` (`linka` nvarchar(50)  not null collate utf8_bin ,`odkud` nvarchar(50)  not null ,`kam` nvarchar(50)  not null ,primary key ( `linka`) ) engine=InnoDb auto_increment=0;
CREATE index  `IX_odkud` on `Trasy` (`odkud` DESC) using HASH;
CREATE index  `IX_kam` on `Trasy` (`kam` DESC) using HASH;
create table `Lokalita` (`nazev` nvarchar(50)  not null collate utf8_bin ,primary key ( `nazev`) ) engine=InnoDb auto_increment=0;
create table `Mezizastavka` (`nazev` nvarchar(50)  not null collate utf8_bin ,`linka` nvarchar(50)  not null collate utf8_bin ,primary key ( `nazev`,`linka`) ) engine=InnoDb auto_increment=0;
CREATE index  `IX_nazev` on `Mezizastavka` (`nazev` DESC) using HASH;
CREATE index  `IX_linka` on `Mezizastavka` (`linka` DESC) using HASH;
create table `Jizdenka` (`linka` nvarchar(50) not null collate utf8_bin, `email` nvarchar(50) collate utf8_bin, `cas` timestamp(5) not null ,`cislo` bigint not null  auto_increment, primary key ( `cislo`) ) engine=InnoDb auto_increment=0;
CREATE index  `IX_cas_linka` on `Jizdenka` (`cas` DESC,`linka` DESC) using HASH;
CREATE index  `IX_email` on `Jizdenka` (`email` DESC) using HASH;
alter table `Autobus` add constraint `FK_Autobus_Znacka_znacka`  foreign key (`znacka`) references `Znacka` ( `znacka`)  on update cascade on delete cascade ;
alter table `Kontakt` add constraint `FK_Kontakt_TypKontaktu_typ`  foreign key (`typ`) references `TypKontaktu` ( `typ`)  on update cascade on delete cascade ;
alter table `Kontakt` add constraint `FK_Kontakt_Ridic_cislo_rp`  foreign key (`cislo_rp`) references `Ridic` ( `cislo_rp`)  on update cascade on delete cascade ;
alter table `Jizda` add constraint `FK_Jizda_Autobus_spz`  foreign key (`spz`) references `Autobus` ( `spz`)  on update cascade on delete cascade ;
alter table `Jizda` add constraint `FK_Jizda_Ridic_cislo_rp`  foreign key (`cislo_rp`) references `Ridic` ( `cislo_rp`)  on update cascade on delete cascade ;
alter table `Jizda` add constraint `FK_Jizda_Trasy_linka`  foreign key (`linka`) references `Trasy` ( `linka`)  on update cascade on delete cascade ;
alter table `Trasy` add constraint `FK_Trasy_Lokalita_kam`  foreign key (`kam`) references `Lokalita` ( `nazev`) ;
alter table `Trasy` add constraint `FK_Trasy_Lokalita_odkud`  foreign key (`odkud`) references `Lokalita` ( `nazev`) ;
alter table `Mezizastavka` add constraint `FK_Mezizastavka_Lokalita_nazev`  foreign key (`nazev`) references `Lokalita` ( `nazev`)  on update cascade on delete cascade ;
alter table `Mezizastavka` add constraint `FK_Mezizastavka_Trasy_linka`  foreign key (`linka`) references `Trasy` ( `linka`)  on update cascade on delete cascade ;
alter table `Jizdenka` add constraint `FK_Jizdenka_Klient_email`  foreign key (`email`) references `Klient` ( `email`) ;
alter table `Jizdenka` add constraint `FK_Jizdenka_Jizda_cas_linka`  foreign key (`cas`,`linka`) references `Jizda` ( `cas`,`linka`) ;
create table `__MigrationHistory` (`MigrationId` nvarchar(150)  not null ,`ContextKey` nvarchar(300)  not null ,`Model` longblob not null ,`ProductVersion` nvarchar(32)  not null ,primary key ( `MigrationId`) ) engine=InnoDb auto_increment=0;
INSERT INTO `__MigrationHistory`(
`MigrationId`, 
`ContextKey`, 
`Model`, 
`ProductVersion`) VALUES (
'201904291720514_Init', 
'RDB.Data.Migrations.Configuration', 
0x1F8B0800000000000400ED5DDD6EDCB815BE2FD07710E6B2C87AECDDA6688DF12E1C3B29B26BC781275914BD3168899EA8D64853FD18B68B3E592FFA487D85522229F1E75022351A699C0DF66263513C3C3F1F8FC873C833FFFBCF7F173F3DAE23EF01A75998C427B3A383C39987633F09C27875322BF2BBEFFE3CFBE9C7DFFF6EF136583F7ABFF2F77E28DF233DE3EC64F625CF37C7F379E67FC16B941DAC433F4DB2E42E3FF093F51C05C9FCFBC3C3BFCC8F8EE6989098115A9EB7B82EE23C5CE3EA0FF2E75912FB78931728BA4C021C65EC3969595654BD0F688DB30DF2F1C9ECFAFCCDC139CAD1C1F9E9C5CC3B8D42447858E2E86EE6A1384E7294130E8F3F677899A749BC5A6EC803147D7ADA60F2DE1D8A32CC383F6E5EB715E2F0FB528879D39193F28B2C4FD68E048F7E605A99ABDD7BE976566B8DE8ED2DD16FFE544A5DE9EE64F6A6C8669E3ACEF1599496EF085AA5063820AFBFF2F8C357B5E1093ECAFF5E7967459417293E897191A7287AE57D2C6EA3D0FF053F7D4AEE717C12175124324458226DD203F2E8639A6C709A3F5DE33BC6E6C708E5C44073B9EB5CED5BF794BB515988E1097A67DE257ABCC0F12AFF72327B4DE0FA2E7CC4017FC080F0390E09D6499F3C2DC89F1F08D7E836C275FBBC75D44A57E53F773FF207F410AE2A9B413CCCBC6B1C55ADD9977043A74369C21BD6FA2E4DD6D7494461401FDE2C9322F54BCE13B5E5134A5738975958CC1B44B5E28C8D688FB4EA7F93608D5ACE156A13DB9B9809677DECCDADAADB9B23A197BDCFA210C7B98BC1698F492CFE768DC2C8DDE4ACDBC8DEE55D9866793C0ADA949197453AC2B8F6104BE21CF96E18A35D2601D9AF282A7AF815D66D645397C38D3EE8791A9215E745E8E338C31F8AF52D4E27F4A80C2B5413805F65ED37D27B8D87059AB56F2BF40EF4956D63932AAD9543FE8ACE1C6D31F2C59AB7FAF04BEA719EA965E324B395F2EB3A59C799365D9085D7013DF0AAAE0DDA30DD0B1C1C99F6B8A03D268184E2985CB131B25FFB8DAD1AB69E169D4ED23419141FDA7F1E384F83691C63A8EF8EC017AF9322EF3D59E82854762220A67F3B224FE26064D4936DD434C1870A19FBB286AAA24BC0A4AB98BCA95A9BF9563FD4D6234DCB80AB234A549FF6E27303272DEBA236662A3CB6F0C2DA5556AAC7064E68DB560B3436AABDEFA93A4C1399E9E94D26F201E77883D25211E3C48694D14F53821034751C92717191F82C820DA09F82587BB39907E00BDA8C80DF72F618DC68162C03EFAA4C6BAF18D8D6DFDB6A52370CD9CF6BDEE75BD0B597CA97B9B3C65997BD51F8F0EBB8493CDFA8EB3EA32369F51FCCF23790E350DB348FA1BD30DC428493D69622528391A30196239F42FF1E3B0575698F17BC20791FE77FFA23803441F8659EA4F8AF38C629D953041F519EE334263D035CE9AF6B46D0DCCA40F90A8BFDC7CBDBBE9983072C91054C160ABC1BFE463359A4066DB2C8ADBD76346DECB017346EE886C1C00C6D7499B8A75996F861C543B3D96389465994B771E019B3CCD4AC2C4F490C4B266AB82153938C7832FB83A61688561D986968B17CA74CED68A64EE8ABF81C4738C7DEA94F4F679CA1CC4701B01021E3CA4F880FC06939F9507446AC40BC4A18E7BAC308633FDCA0C8C4B3D2C1E5433DAF89AB2D642989E3D23198B46E33AA7086411FBA1E41D152975216730135ED6082A2D52628B4A65A1A50D4F9427B90B506C535CA34A4BE6FA06B91C106085022C4097E2DD6D966FC1131C862445D2051434683204F0D3E374479E46A5FF126736E63EAD6044B2FCCC926B1E101CC414F82BE26466AC208103055E0E182383DC8DAFE719E1E691AC73606064F123A814B53BBCDB04D3E60423075383238F2BD35A45E9E0B83F81ED78141A6B0765FFBE0BDE098AE09281D01DE06312C2E610FC1F6A0B040B989C4A8685489F782542B1FA36C035A756CC38194509810547AD4BDDDF82D21F8AD81650EDB8F0D2D23272382CBA8692BD72567CB26FC4C522CB47FD49438E8D61F493972DA05CC7DF9444A5C5B016DA84FA3A47F9B915BD21823814B8BF99B10614E0034B0A8B34DF64033A60DAC5CD5D48033713F8A7B3359649BC147445CBB4333A47606C1DA4B736B20DF233936D00E2FC3B5C9991013280C699106143CF7678F3338972286DE5872462679787030D0E20B64C0C668E09D2327BC80EAB4195ACA064E8917BA56EAB0AD92B71A002D72B2AB6BED362C50A4B16D8C059FB7EDE3075C812529DE3AF8301CBF83E3902632ABD87018E3B4DE77DC2162F0F2317E840E1E7CCE303B7B90B16CB28AAF92EC12E7F255CC266D2AC650356CCA7DE99106A033CB687674A7331BEACFFD601781FAA8BC4E812731EC4854FA329301EFD7A8A468FC0BA2C2238736048CFD3BBB570085BAB3D54B4777BE5A8428340BED0E226C5500D1A8D7651D24E87C86287017AA1010E68F046B7E8F5868D72E19ABD3B82DFB5F335A4F1ACD0BB425FC85EE7CDEA8AB4259120B29C15B52BABC9DE96989F5B604B5204433F95AD4D09692D669B15938985AF8BC336B04CA318002287986BE7A50B20B0299DA7B6C2DBD705D4117DC90A703623C62A64EE5B35D583D37D735757A4B69B6B0398704F06AB6AEB5B86358D670F45C17DE229FE190D11064E1DF981675B4E730045AC2076720CD0027DC4DBAE908CB3B05E6FBE8C71C8ADF9186A44B3BA6E902845E3AA3C97D268B1C68E9D45F0F71F523CBBACCED214EBB20A7C07BB3F66991DF18D6DC91D99523D2662574991E8EBBF5157FF700508EBBEA82B744812CE24002CBF582B5456E38F223AE22F85E6828C1D99EC1283710CDE88E67F4935A8E6074FA8B1699F9F1DE7A575CB72DE6B4C81A7BB0981BAAB12D2ED16613C62BA13A1B7BE22D6969B6B3EF96EE95CBD694C6DC97D655EA1EBE1E294F52B4C24A6BB94A0C7075F5BDBC18708BCA83DF67C15A7B4D890118B6537C30719BAF9B8A6FB1F8DBE5BFD92755285177002C181BCDBD23C2ACCB804C75EE5F5EEBE9BDBCB21E1E8A500A573F3B4BA2621DD347D9E6190ACE982808A780452ACF31F2EF1118E5992B1268511D4D415A1C5FD6B6952DD8F6AFB731C0ADAB85390CFD4CEA7C019AE45EB3B72A61EF6FA14B5347933259FC5AD4268643DA661A42550C91CE3F0883890B9DBAC6854865938625A1709FACCB77D6FDCDCBA2803DEC6BEA69522AAB7C25AAF44B1294615917D3D0088E48247FDAB810004F048BF4FC308B929B14243AAD9D69F4675B5B43C12D7B7BC3BDB7B6D6448AE59187DE3A85C32716EA34753469B22760BF794A2973B09D9DFB9AD961BE84AA2E7D04EC20CC04A41C9C48270A63786162A2D49C70DF62C1099D5FDE735FCBF6D7BD910286092C9062E8675C7B0E6163E58CA6482B09EE8BC08596749C58A4748FD67B64DE2674D4DBC2A668988591CD5DEDF718317AC60F7BA4D13AACD55BA13CF1E9AE4F63CF01D4399C8B9DC8303C00D5DB2E2C9DEC6E1653471767567D1A5CEC221D83DA6A1B291C7C19F553BC539C689141F5957AF43A42A84402172C2AD7FDE30D5A988EBE32F3888A1EC2A00CD15D3E2DFF19B1BA1FE53F79189ABF7189E2F00E67392DE8317B7DF05AF91588FDF9458679960552D408FE5906D95456F54880D556773192AA133D14123FA0D4FF8252ADC28663E90E1ED652A96A67ABDEC7017E3C99FDABEA77ECBDFFDB0DEDFACABB4A89518FBD43EFDF6ECC38FE22410F25C331BB6E3D9B94B213E9C0A3B856E281DEAF5B3A2C1697190A476C7B392CD166B7398E25A0B8979D290CE1AE6E63D41D87D55C158BE931A749BFFE135A67A3D904F6E085771EC1C3986B9758591F887C755B1E34D14EA4032F935B0966DAC4774B67B6FC373F031DA6B73307B44C05DE0397A116362BC9530D94BFED95E568BD29976D9818B35A80B96B9671D263F6573D87F445E0E2C98211D2EFB7E812A19B685620ED093E0352B653360B75F5D074D57348BB57B1B21E8C907E2358DB70D3D5CAE06098A5DBE0ACDB281E170C266D21DD90C037E8C1021A55CF21313A99B776AD39DA772933D437D1C655A30CD2CA445F539D9BA3813E659C9DDB701596519ECEFAA78E831876AB1632573D2D31A9D74B1DB29A26BB8CA35F12DBE1A56AE93A8E7BF9CE5E37274D87B36C23339ECBDD47F8589ECB50FB591C931EAC30DDC31B0531F50542E852E14E90D37E80C56AC7ED041EF3E128EBB1F6A7AC25BF0804DCBAFC4AE1623C9B631FC2D8195CDA06DC8B629416DF831D01859DCBEFBAC73F10486C3F10E0C117D73247DB8CB457552527F423E3C2636C1F620F93893D885B41C8E692197867BEAB42877237B04FA5BF5ED66F39E9631F0370ACBA67351678A66A2F6B367EB3FCB096379CCBDB9B728A9666FA2A3E0EF656331CF9D9C987C138D63ED54474740B3B014B5D6F05AAC1F2357815F3B94A87D1F6A6AEE1B49E657CB08CE95D5C9032A57FB12B4CC8CF128A9106FBBA81EAEDEC5E45EAFAC5B74C772D2D4F753999DC78B8D761B07D292D68F9D9DF275B3B6CF4E063C73BF1030EA01880AD912A02EA45035410C8F5D5AA505793D7110360F41CF3C92CB82D535A342B745AE4C9AD6D2D4073294088F4DFE1A3A92A65EEDC34D2BC01A2FD8B5305C1B6028220F5B2F1DEA9BA605771416818D2C2462A3A87E2512A6D14DE000D701D06A16F47DA5C961022FC73F81C745B96AD7034C2EC39A8921465FA0F48AA849BA5B646BB6982C85F24F7280A8143ACEA08F5E2481BA06E81E85FE2E7F01965397AB0003EF7D0DA10BCC1A4790C1DC2D84D994469DA0B5526747FA804DF851E50A9D1FDAA85284E60EDBA789BA88A7F914A0B0C27F200750E65F721DEE19E4EBCAD0B195A000D5A68A93A184C9461AA153A5B697762EDAC20A1EAC0E5EBA56DC24ADF14E1EAF150C2EEA8C6E03E0A3C48C9404BF64602ECC06501FB5B4D593F48D77D871373EBAA7F7DACB75BD106ABEB276F2BC4A24E6DC2C96B32F13AF060A26D5BB9CF72F60C2E9643713EFDAA2DD916177179BA94FE758EB370D59058109A31F6A50D71FDCEFBF82EE1DB728523FE8A5A280EE72820BBE5D3340FEFC85A8534FB38CBAA1F6BAFEA3895F5BA6E71F03EBE2AF24D911391F1FA3692F61DE5FEBE6DFCAA02A1CCF3E26A53FE950D210261332422E0ABF84D114641CDF73BE060B1814419386087544B5BE6E561D5D5534DE943125B1262EAABE31D9FF07A53D656C9AEE2252A81E7CEDBE70C5FE015F29FF8856933916E43C86A5F9C876895A275C66834FDC99F04C3C1FAF1C7FF03A8595EDA54A00000, 
'6.2.0-61023');
  END IF;

END //

DELIMITER ;

CALL `_idempotent_script`();

DROP PROCEDURE IF EXISTS `_idempotent_script`;

