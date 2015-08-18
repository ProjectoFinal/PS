
select * from _User


select * from Person
insert into Person values('jacky',GETDATE(),'Portugal','Alfornelos')





drop proc filterPerson

CREATE PROCEDURE filterPerson
@npage INT,
 @table nvarchar(50) ,
 @filter nvarchar(50) ,
 @column nvarchar(50) ,
 @result  nvarchar(50) out 
 
 As 

 
DECLARE @id int, @name nvarchar(50),@birthday nvarchar(50),@birthdayPlace nvarchar(50),@adress nvarchar(50),
 @nrow int=0,@Limit int=2;

execute('Declare cursorAux cursor for select * from '+ @table+' where '+@column+'='''+@filter+'''')
 
	
	set @nrow=@npage*@Limit;

open cursorAux

FETCH NEXT FROM cursorAux INTO @id, @name,@birthday,@birthdayPlace,@adress

WHILE (@@FETCH_STATUS = 0 and @nrow>0)
BEGIN


set  @result=@result+' name:'+@name+' birthay:'+@birthday+' birthdayPlace:'+@birthdayPlace+' adress:'+@adress;
print @result
set @nrow=@nrow-1;
FETCH NEXT FROM cursorAux INTO @id, @name,@birthday,@birthdayPlace,@adress

end

close cursorAux
DEALLOCATE cursorAux

go


DECLARE  @aux  nvarchar(50)=' ';

Execute filterPerson 1,'Person','jacky','name' ,@aux


print @aux