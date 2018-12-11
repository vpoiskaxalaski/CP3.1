
EXEC CreateBillTable_sp

DECLARE
@clientId int = 29,
@bill money

EXEC GetClientBill @clientId, @bill out
print @bill

use hotel
select * from ##bill

DECLARE
@start datetime = '01-01-2018',
@end datetime = '01-01-2019',
@income money 
EXEC DeterminateIncome_sp @start, @end, @income out
PRINT @income

