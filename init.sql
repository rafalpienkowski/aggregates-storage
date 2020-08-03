/* Entity Framework*/

CREATE SCHEMA IF NOT exists ef;

CREATE TABLE IF NOT EXISTS ef."AccountOwners" (
	"Id" uuid NOT NULL,
	"Name" varchar(80) NOT NULL,
	CONSTRAINT accountowners_pkey PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS ef."CreditCards" (
	"Id" uuid NOT NULL,
	"OwnerId" uuid NOT NULL REFERENCES ef."AccountOwners"("Id"),
	"Limit" decimal(10,2) NULL,
	CONSTRAINT creditcards_pkey PRIMARY KEY ("Id")
);

/* Snapshot */

CREATE SCHEMA IF NOT exists snapshot;

CREATE TABLE IF NOT EXISTS snapshot."AccountOwners" (
	"Id" uuid NOT NULL,
	"Name" varchar(80) NOT NULL,
	CONSTRAINT accountowners_pkey PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS snapshot."CreditCards" (
	"Id" uuid NOT NULL,
	"OwnerId" uuid NOT NULL REFERENCES snapshot."AccountOwners"("Id"),
	"AvaliableLimit" decimal(10,2) NULL,
	CONSTRAINT creditcards_pkey PRIMARY KEY ("Id")
);

/* MARTEN */

CREATE SCHEMA IF NOT exists event_store;