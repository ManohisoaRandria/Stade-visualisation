
create sequence seq_espace;
CREATE TABLE espace(
	idEspace Varchar(50) PRIMARY KEY,
	nomEspace Varchar(50),
	pointsEspace text
);


create sequence seq_spectacle;
CREATE TABLE spectacle (
    idSpectacle Varchar(50) PRIMARY KEY,
    nomSpectacle Varchar(50),
    idEspace Varchar(50),
    dateSpectacle date,
    FOREIGN KEY (idEspace) REFERENCES espace(idEspace)
);

create sequence seq_configspectacle;
CREATE TABLE configSpectacle (
    idConfigSpectacle Varchar(50) PRIMARY KEY,
	idSpectacle Varchar(50),
	idZone Varchar(50),
	debutNum integer,
    sensHorizontal Varchar(5),
	sensVertical Varchar(5),
	direction Varchar(5),
	FOREIGN KEY (idZone) REFERENCES zone(idZone),
    FOREIGN KEY (idSpectacle) REFERENCES spectacle(idSpectacle)
);

create sequence seq_zone;
CREATE TABLE zone (
    idZone Varchar(50) PRIMARY KEY,
    nomZone Varchar(20),
    idEspace Varchar(50),
    puZone decimal(50,2),
	lngSeza integer,
    largSeza integer,
    ecartEntreSeza integer,
    pointZone text,
    FOREIGN KEY (idEspace) REFERENCES espace(idEspace)
);


create sequence seq_seza;
CREATE TABLE Seza (
    idSeza Varchar(50) PRIMARY KEY,
    idZone Varchar(50),
	rang integer,
    x integer,
    y integer,
    lng integer,
    larg integer,
    etat integer,
    FOREIGN KEY (idZone) REFERENCES zone(idZone)
);
select zone.idzone as idzone,
	nomzone,
	idespace,
	puzone,
	lngSeza,
    largSeza,
    ecartEntreSeza,
	pointZone,
	idseza,
	rang

	

create sequence seq_numseza;
CREATE TABLE numSeza (
    idNumSeza Varchar(50) PRIMARY KEY,
    idSeza Varchar(50),
	idSpectacle Varchar(50),
	num integer,
    FOREIGN KEY (idSeza) REFERENCES Seza(idSeza),
	FOREIGN KEY (idSpectacle) REFERENCES spectacle(idSpectacle)
);

create sequence seq_reservation;
CREATE TABLE reservation (
    idReservation Varchar(50) PRIMARY KEY,
    dateReservation Date,
    idSeza Varchar(50),
    idSpectacle Varchar(50),
	prixReservation decimal(50,2),
    etat integer,
    FOREIGN KEY (idSpectacle) REFERENCES spectacle(idSpectacle),
    FOREIGN KEY (idSeza) REFERENCES seza(idSeza)
);


create sequence seq_simulation;
CREATE TABLE simulation (
    idSimulation Varchar(50) PRIMARY KEY,
    idZone Varchar(50),
    pourcentage decimal(50,2),
    idSpectacle Varchar(50),
    prixSimulation decimal(50,2),
    FOREIGN KEY (idZone) REFERENCES zone(idZone),
    FOREIGN KEY (idSpectacle) REFERENCES spectacle(idSpectacle)
); 


create sequence seq_media;
CREATE TABLE media (
    idMedia Varchar(50) PRIMARY KEY,
    nomMedia Varchar(50)
);


create sequence seq_pub;
CREATE TABLE pub (
    idPub Varchar(50) PRIMARY KEY,
    datePub date,
    idMedia Varchar(50),
	idSpectacle Varchar(50),
	FOREIGN KEY (idMedia) REFERENCES media(idMedia),
    FOREIGN KEY (idSpectacle) REFERENCES spectacle(idSpectacle)
);


----VIEW
------Super requete
--SELECT  seza.id,
--        seza.zone,
--        rang,
--        numero,
--        longueur,
--        largeur,
--        x,
--        y, 
--        CASE 
--				WHEN reservation.spectacle = 'sp3' THEN reservation.etat
--				ELSE seza.etat
--			END as etat
--FROM seza left join (select * from reservation where spectacle = 'sp3') as reservation on reservation.seza = seza.id; 

--CREATE VIEW NbSezaParReservation as
--SELECT  zone.id as zone,
--        reservation.spectacle,
--        reservation.datereserve,
--        count(*) as nbSeza
--FROM reservation,seza,zone 
--WHERE   reservation.seza = seza.id 
--        and seza.zone = zone.id
--        group by reservation.spectacle,zone.id,reservation.datereserve order by datereserve;