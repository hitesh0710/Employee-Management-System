alter table Username drop column ID ;
alter table Username ADD ID  INT IDENTITY(1,1);