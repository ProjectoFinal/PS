insert into _User VALUES ('scarops','lausdeo')
insert into _User VALUES ('jacky','123456')
insert into _User VALUES ('jaqueline','20081991')


select * from _User


insert into _Role VALUES ('r2',null)
insert into _Role VALUES ('r1','r2')


select * from _Role

insert into _Permission values ('POST','/Person')
insert into _Permission values ('GET','/Person')

select * from _Permission

insert into _UserAssignment values ('scarops','r1')
insert into _UserAssignment values ('jacky','r2')
insert into _UserAssignment values ('jaqueline','r1')


select * from _UserAssignment

insert into _PermissionAssignment values ('POST','/Person','r1')
insert into _PermissionAssignment values ('GET','/Person','r2')

select * from _PermissionAssignment

