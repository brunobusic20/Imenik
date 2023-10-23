use master;
drop database if exists imenik;
go
create database imenik;
go
use imenik;
select * from kontakt;


create table kontakt(
	sifra int not null primary key identity(1,1),
	ime varchar(50) not null,
	prezime varchar(50) not null,
	broj char(10),
	adresa varchar(50) not null

)




insert into kontakt(ime,prezime,broj,adresa)
values('Bruno','Buši?','0998474271','Petefija Sandora 16');



insert into kontakt(ime,prezime,broj,adresa)
values
('Angela','Buši?','0913969853','Petefija Sandora 16'),
('Gabriela','Buši?','0917655008','Petefija Sandora 16');



