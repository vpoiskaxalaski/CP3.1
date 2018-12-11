@echo off

sqlcmd -S Cloud\SQLExpress -i E:/University/3k1s/DatabaseAdministrationCP/CONFIG/FreeTheRoom.sql E:/University/3k1s/DatabaseAdministrationCP/CONFIG/ClearClientServices.sql -d hotel -U  admin -P Admin99
