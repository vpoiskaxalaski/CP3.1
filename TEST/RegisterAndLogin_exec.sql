declare @x xml = '<?xml version="1.0"?>
<CLIENT>
  <ID>0</ID>
  <FIRST_NAME>Yulia Hit</FIRST_NAME>
  <MAIL>yulia@mail.ru</MAIL>
  <PASWRD>defw4fw5</PASWRD>
</CLIENT>'

--exec RegisterUser @x  

--select * from CLIENT

exec Login_sp @x
