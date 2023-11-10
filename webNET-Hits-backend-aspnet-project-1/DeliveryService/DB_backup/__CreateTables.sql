--Table: fias.as_addr_obj

-- DROP TABLE IF EXISTS fias.as_addr_obj;

CREATE TABLE IF NOT EXISTS fias.as_addr_obj
(
    id bigint NOT NULL,
    objectid bigint NOT NULL,
    objectguid uuid NOT NULL,
    changeid bigint,
    name text COLLATE pg_catalog."default" NOT NULL,
    typename text COLLATE pg_catalog."default",
    level text COLLATE pg_catalog."default" NOT NULL,
    opertypeid integer,
    previd bigint,
    nextid bigint,
    updatedate date,
    startdate date,
    enddate date,
    isactual integer,
    isactive integer,
    CONSTRAINT "PK_Addr_Objs" PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS fias.as_addr_obj
    OWNER to postgres;

COMMENT ON TABLE fias.as_addr_obj
    IS 'Сведения классификатора адресообразующих элементов';

COMMENT ON COLUMN fias.as_addr_obj.id
    IS 'Уникальный идентификатор записи. Ключевое поле';

COMMENT ON COLUMN fias.as_addr_obj.objectid
    IS 'Глобальный уникальный идентификатор адресного объекта типа INTEGER';

COMMENT ON COLUMN fias.as_addr_obj.objectguid
    IS 'Глобальный уникальный идентификатор адресного объекта типа UUID';

COMMENT ON COLUMN fias.as_addr_obj.changeid
    IS 'ID изменившей транзакции';

COMMENT ON COLUMN fias.as_addr_obj.name
    IS 'Наименование';

COMMENT ON COLUMN fias.as_addr_obj.typename
    IS 'Краткое наименование типа объекта';

COMMENT ON COLUMN fias.as_addr_obj.level
    IS 'Уровень адресного объекта';

COMMENT ON COLUMN fias.as_addr_obj.opertypeid
    IS 'Статус действия над записью – причина появления записи';

COMMENT ON COLUMN fias.as_addr_obj.previd
    IS 'Идентификатор записи связывания с предыдущей исторической записью';

COMMENT ON COLUMN fias.as_addr_obj.nextid
    IS 'Идентификатор записи связывания с последующей исторической записью';

COMMENT ON COLUMN fias.as_addr_obj.updatedate
    IS 'Дата внесения (обновления) записи';

COMMENT ON COLUMN fias.as_addr_obj.startdate
    IS 'Начало действия записи';

COMMENT ON COLUMN fias.as_addr_obj.enddate
    IS 'Окончание действия записи';

COMMENT ON COLUMN fias.as_addr_obj.isactual
    IS 'Статус актуальности адресного объекта ФИАС';

COMMENT ON COLUMN fias.as_addr_obj.isactive
    IS 'Признак действующего адресного объекта';

-- Table: fias.as_adm_hierarchy

-- DROP TABLE IF EXISTS fias.as_adm_hierarchy;

CREATE TABLE IF NOT EXISTS fias.as_adm_hierarchy
(
    id bigint NOT NULL,
    objectid bigint,
    parentobjid bigint,
    changeid bigint,
    regioncode text COLLATE pg_catalog."default",
    areacode text COLLATE pg_catalog."default",
    citycode text COLLATE pg_catalog."default",
    placecode text COLLATE pg_catalog."default",
    plancode text COLLATE pg_catalog."default",
    streetcode text COLLATE pg_catalog."default",
    previd bigint,
    nextid bigint,
    updatedate date,
    startdate date,
    enddate date,
    isactive integer,
    path text COLLATE pg_catalog."default",
    CONSTRAINT "PK_Adm_Hier" PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS fias.as_adm_hierarchy
    OWNER to postgres;

COMMENT ON TABLE fias.as_adm_hierarchy
    IS 'Сведения по иерархии в административном делении';

COMMENT ON COLUMN fias.as_adm_hierarchy.id
    IS 'Уникальный идентификатор записи. Ключевое поле';

COMMENT ON COLUMN fias.as_adm_hierarchy.objectid
    IS 'Глобальный уникальный идентификатор объекта';

COMMENT ON COLUMN fias.as_adm_hierarchy.parentobjid
    IS 'Идентификатор родительского объекта';

COMMENT ON COLUMN fias.as_adm_hierarchy.changeid
    IS 'ID изменившей транзакции';

COMMENT ON COLUMN fias.as_adm_hierarchy.regioncode
    IS 'Код региона';

COMMENT ON COLUMN fias.as_adm_hierarchy.areacode
    IS 'Код района';

COMMENT ON COLUMN fias.as_adm_hierarchy.citycode
    IS 'Код города';

COMMENT ON COLUMN fias.as_adm_hierarchy.placecode
    IS 'Код населенного пункта';

COMMENT ON COLUMN fias.as_adm_hierarchy.plancode
    IS 'Код ЭПС';

COMMENT ON COLUMN fias.as_adm_hierarchy.streetcode
    IS 'Код улицы';

COMMENT ON COLUMN fias.as_adm_hierarchy.previd
    IS 'Идентификатор записи связывания с предыдущей исторической записью';

COMMENT ON COLUMN fias.as_adm_hierarchy.nextid
    IS 'Идентификатор записи связывания с последующей исторической записью';

COMMENT ON COLUMN fias.as_adm_hierarchy.updatedate
    IS 'Дата внесения (обновления) записи';

COMMENT ON COLUMN fias.as_adm_hierarchy.startdate
    IS 'Начало действия записи';

COMMENT ON COLUMN fias.as_adm_hierarchy.enddate
    IS 'Окончание действия записи';

COMMENT ON COLUMN fias.as_adm_hierarchy.isactive
    IS 'Признак действующего адресного объекта';

COMMENT ON COLUMN fias.as_adm_hierarchy.path
    IS 'Материализованный путь к объекту (полная иерархия)';

-- Table: fias.as_houses

-- DROP TABLE IF EXISTS fias.as_houses;

CREATE TABLE IF NOT EXISTS fias.as_houses
(
    id bigint NOT NULL,
    objectid bigint NOT NULL,
    objectguid uuid NOT NULL,
    changeid bigint,
    housenum text COLLATE pg_catalog."default",
    addnum1 text COLLATE pg_catalog."default",
    addnum2 text COLLATE pg_catalog."default",
    housetype integer,
    addtype1 integer,
    addtype2 integer,
    opertypeid integer,
    previd bigint,
    nextid bigint,
    updatedate date,
    startdate date,
    enddate date,
    isactual integer,
    isactive integer,
    CONSTRAINT "PK_Houses" PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS fias.as_houses
    OWNER to postgres;

COMMENT ON TABLE fias.as_houses
    IS 'Сведения по номерам домов улиц городов и населенных пунктов';

COMMENT ON COLUMN fias.as_houses.id
    IS 'Уникальный идентификатор записи. Ключевое поле';

COMMENT ON COLUMN fias.as_houses.objectid
    IS 'Глобальный уникальный идентификатор объекта типа INTEGER';

COMMENT ON COLUMN fias.as_houses.objectguid
    IS 'Глобальный уникальный идентификатор адресного объекта типа UUID';

COMMENT ON COLUMN fias.as_houses.changeid
    IS 'ID изменившей транзакции';

COMMENT ON COLUMN fias.as_houses.housenum
    IS 'Основной номер дома';

COMMENT ON COLUMN fias.as_houses.addnum1
    IS 'Дополнительный номер дома 1';

COMMENT ON COLUMN fias.as_houses.addnum2
    IS 'Дополнительный номер дома 1';

COMMENT ON COLUMN fias.as_houses.housetype
    IS 'Основной тип дома';

COMMENT ON COLUMN fias.as_houses.addtype1
    IS 'Дополнительный тип дома 1';

COMMENT ON COLUMN fias.as_houses.addtype2
    IS 'Дополнительный тип дома 2';

COMMENT ON COLUMN fias.as_houses.opertypeid
    IS 'Статус действия над записью – причина появления записи';

COMMENT ON COLUMN fias.as_houses.previd
    IS 'Идентификатор записи связывания с предыдущей исторической записью';

COMMENT ON COLUMN fias.as_houses.nextid
    IS 'Идентификатор записи связывания с последующей исторической записью';

COMMENT ON COLUMN fias.as_houses.updatedate
    IS 'Дата внесения (обновления) записи';

COMMENT ON COLUMN fias.as_houses.startdate
    IS 'Начало действия записи';

COMMENT ON COLUMN fias.as_houses.enddate
    IS 'Окончание действия записи';

COMMENT ON COLUMN fias.as_houses.isactual
    IS 'Статус актуальности адресного объекта ФИАС';

COMMENT ON COLUMN fias.as_houses.isactive
    IS 'Признак действующего адресного объекта';
-- Table: public.BanedTokens

-- DROP TABLE IF EXISTS public."BanedTokens";

CREATE TABLE IF NOT EXISTS public."BanedTokens"
(
    token text COLLATE pg_catalog."default" NOT NULL,
    expiredtime timestamp with time zone NOT NULL,
    CONSTRAINT "PK_BanedTokens" PRIMARY KEY (token)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."BanedTokens"
    OWNER to postgres;
-- Table: public.DishBasket

-- DROP TABLE IF EXISTS public."DishBasket";

CREATE TABLE IF NOT EXISTS public."DishBasket"
(
    "Id" uuid NOT NULL,
    "UserId" uuid NOT NULL,
    "DishId" uuid NOT NULL,
    "Name" text COLLATE pg_catalog."default" NOT NULL,
    "Price" double precision NOT NULL,
    "Amount" integer NOT NULL,
    "Image" text COLLATE pg_catalog."default",
    "OrderId" uuid,
    CONSTRAINT "PK_DishBasket" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_DishBasket_Users_UserId" FOREIGN KEY ("UserId")
        REFERENCES public."Users" ("NameId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."DishBasket"
    OWNER to postgres;
-- Index: IX_DishBasket_UserId

-- DROP INDEX IF EXISTS public."IX_DishBasket_UserId";

CREATE INDEX IF NOT EXISTS "IX_DishBasket_UserId"
    ON public."DishBasket" USING btree
    ("UserId" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Table: public.DishInBaskets

-- DROP TABLE IF EXISTS public."DishInBaskets";

CREATE TABLE IF NOT EXISTS public."DishInBaskets"
(
    "Id" uuid NOT NULL,
    "UserId" uuid NOT NULL,
    "DishId" uuid NOT NULL,
    "Name" text COLLATE pg_catalog."default" NOT NULL,
    "Price" double precision NOT NULL,
    "Amount" integer NOT NULL,
    "Image" text COLLATE pg_catalog."default",
    "OrderId" uuid,
    CONSTRAINT "PK_DishInBaskets" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_DishInBaskets_Users_UserId" FOREIGN KEY ("UserId")
        REFERENCES public."Users" ("NameId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."DishInBaskets"
    OWNER to postgres;
-- Index: IX_DishInBaskets_UserId

-- DROP INDEX IF EXISTS public."IX_DishInBaskets_UserId";

CREATE INDEX IF NOT EXISTS "IX_DishInBaskets_UserId"
    ON public."DishInBaskets" USING btree
    ("UserId" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Table: public.Dishes

-- DROP TABLE IF EXISTS public."Dishes";

CREATE TABLE IF NOT EXISTS public."Dishes"
(
    "Id" uuid NOT NULL,
    "Name" text COLLATE pg_catalog."default" NOT NULL,
    "Description" text COLLATE pg_catalog."default" DEFAULT ''::text,
    "Price" double precision NOT NULL,
    "Rating" double precision,
    "Image" text COLLATE pg_catalog."default" DEFAULT ''::text,
    "IsVegetarian" boolean NOT NULL,
    "CategoryId" integer NOT NULL DEFAULT 0,
    CONSTRAINT "PK_Dishes" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Dishes"
    OWNER to postgres;
-- Table: public.Orders

-- DROP TABLE IF EXISTS public."Orders";

CREATE TABLE IF NOT EXISTS public."Orders"
(
    "Id" uuid NOT NULL,
    "DeliveryTime" timestamp with time zone NOT NULL,
    "OrderTime" timestamp with time zone NOT NULL,
    "StatusId" integer NOT NULL,
    "Price" double precision NOT NULL,
    "Address" uuid NOT NULL,
    "UserId" uuid NOT NULL,
    CONSTRAINT "PK_Orders" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Orders"
    OWNER to postgres;
-- Table: public.Ratings

-- DROP TABLE IF EXISTS public."Ratings";

CREATE TABLE IF NOT EXISTS public."Ratings"
(
    "Id" uuid NOT NULL,
    "DishId" uuid NOT NULL,
    "UserId" uuid NOT NULL,
    "RatingScore" integer NOT NULL,
    CONSTRAINT "PK_Ratings" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Ratings"
    OWNER to postgres;
-- Table: public.Users

-- DROP TABLE IF EXISTS public."Users";

CREATE TABLE IF NOT EXISTS public."Users"
(
    "NameId" uuid NOT NULL,
    "dbName" text COLLATE pg_catalog."default" NOT NULL,
    "FullName" text COLLATE pg_catalog."default" NOT NULL,
    email text COLLATE pg_catalog."default" NOT NULL,
    password text COLLATE pg_catalog."default" NOT NULL,
    "Gender" integer NOT NULL,
    "BirthDate" timestamp with time zone,
    "AddressId" uuid,
    "phoneNumber" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_Users" PRIMARY KEY ("NameId")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Users"
    OWNER to postgres;
-- Table: public.__EFMigrationsHistory

-- DROP TABLE IF EXISTS public."__EFMigrationsHistory";

CREATE TABLE IF NOT EXISTS public."__EFMigrationsHistory"
(
    "MigrationId" character varying(150) COLLATE pg_catalog."default" NOT NULL,
    "ProductVersion" character varying(32) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."__EFMigrationsHistory"
    OWNER to postgres;