
select * from _User


select * from Person
insert into Person values('jacky',GETDATE(),'Portugal','Alfornelos')





drop proc filterPerson

CREATE PROCEDURE pagining
 @npage INT,
 @limit INT,
 @select nvarchar(100) ,
 @result  nvarchar(50) out 
 
 As 

execute('Declare cursorAux cursor for '+ @select )
 
	
declare @nrow int = @npage * @limit;

open cursorAux

FETCH NEXT FROM cursorAux INTO @id, @name,@birthday,@birthdayPlace,@adress

WHILE (@@FETCH_STATUS = 0 and @nrow > 0)
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