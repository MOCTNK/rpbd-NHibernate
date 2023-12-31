toc.dat                                                                                             0000600 0004000 0002000 00000041713 14332211031 0014433 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        PGDMP       $                
    z            currency_exchanger    13.4    13.4 7    �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false         �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false         �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false         �           1262    73875    currency_exchanger    DATABASE     o   CREATE DATABASE currency_exchanger WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'Russian_Russia.1251';
 "   DROP DATABASE currency_exchanger;
                student    false         �            1255    73966    delete_cashiers()    FUNCTION     �   CREATE FUNCTION public.delete_cashiers() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
DELETE FROM transactions WHERE transactions.id_cashier = OLD.id;
RETURN OLD;
END;
$$;
 (   DROP FUNCTION public.delete_cashiers();
       public          student    false         �            1255    73964    delete_clients()    FUNCTION     �   CREATE FUNCTION public.delete_clients() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
DELETE FROM transactions WHERE transactions.id_client = OLD.id;
RETURN OLD;
END;
$$;
 '   DROP FUNCTION public.delete_clients();
       public          student    false         �            1255    73968    delete_currencies()    FUNCTION       CREATE FUNCTION public.delete_currencies() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
DELETE FROM transactions WHERE transactions.id_currency_sold = OLD.id;
DELETE FROM transactions WHERE transactions.id_currency_purchased = OLD.id;
DELETE FROM rates WHERE rates.id_currency_sold = OLD.id;
DELETE FROM rates WHERE rates.id_currency_purchased = OLD.id;
RETURN OLD;
END;
$$;
 *   DROP FUNCTION public.delete_currencies();
       public          student    false         �            1255    73970    delete_rates()    FUNCTION     �   CREATE FUNCTION public.delete_rates() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
DELETE FROM transactions WHERE transactions.id_rate_sold = OLD.id;
DELETE FROM transactions WHERE transactions.id_rate_purchased = OLD.id;
RETURN OLD;
END;
$$;
 %   DROP FUNCTION public.delete_rates();
       public          student    false         �            1259    73886    cashiers    TABLE     �   CREATE TABLE public.cashiers (
    id integer NOT NULL,
    name character varying(50) NOT NULL,
    surname character varying(50) NOT NULL,
    patronymic character varying(50) NOT NULL
);
    DROP TABLE public.cashiers;
       public         heap    student    false         �            1259    73884    cashiers_id_seq    SEQUENCE     �   CREATE SEQUENCE public.cashiers_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 &   DROP SEQUENCE public.cashiers_id_seq;
       public          student    false    203         �           0    0    cashiers_id_seq    SEQUENCE OWNED BY     C   ALTER SEQUENCE public.cashiers_id_seq OWNED BY public.cashiers.id;
          public          student    false    202         �            1259    73878    clients    TABLE     #  CREATE TABLE public.clients (
    id integer NOT NULL,
    name character varying(50) NOT NULL,
    surname character varying(50) NOT NULL,
    patronymic character varying(50) NOT NULL,
    passport_series character varying(4) NOT NULL,
    passport_number character varying(6) NOT NULL
);
    DROP TABLE public.clients;
       public         heap    student    false         �            1259    73876    clients_id_seq    SEQUENCE     �   CREATE SEQUENCE public.clients_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 %   DROP SEQUENCE public.clients_id_seq;
       public          student    false    201         �           0    0    clients_id_seq    SEQUENCE OWNED BY     A   ALTER SEQUENCE public.clients_id_seq OWNED BY public.clients.id;
          public          student    false    200         �            1259    73894 
   currencies    TABLE     �   CREATE TABLE public.currencies (
    id integer NOT NULL,
    code character varying(3) NOT NULL,
    name character varying(3) NOT NULL
);
    DROP TABLE public.currencies;
       public         heap    student    false         �            1259    73892    currencies_id_seq    SEQUENCE     �   CREATE SEQUENCE public.currencies_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public.currencies_id_seq;
       public          student    false    205         �           0    0    currencies_id_seq    SEQUENCE OWNED BY     G   ALTER SEQUENCE public.currencies_id_seq OWNED BY public.currencies.id;
          public          student    false    204         �            1259    73902    rates    TABLE     �   CREATE TABLE public.rates (
    id integer NOT NULL,
    id_currency_sold integer NOT NULL,
    id_currency_purchased integer NOT NULL,
    sale_rate numeric NOT NULL,
    purchase_rate numeric NOT NULL
);
    DROP TABLE public.rates;
       public         heap    student    false         �            1259    73900    rates_id_seq    SEQUENCE     �   CREATE SEQUENCE public.rates_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 #   DROP SEQUENCE public.rates_id_seq;
       public          student    false    207         �           0    0    rates_id_seq    SEQUENCE OWNED BY     =   ALTER SEQUENCE public.rates_id_seq OWNED BY public.rates.id;
          public          student    false    206         �            1259    74017    transactions    TABLE     �  CREATE TABLE public.transactions (
    id integer NOT NULL,
    id_currency_sold integer NOT NULL,
    id_currency_purchased integer NOT NULL,
    id_client integer NOT NULL,
    id_cashier integer NOT NULL,
    id_rate_sold integer NOT NULL,
    id_rate_purchased integer NOT NULL,
    date_of_transaction date NOT NULL,
    time_of_transaction time without time zone NOT NULL,
    sum_currency_sold numeric DEFAULT 0,
    sum_currency_purchased numeric DEFAULT 0
);
     DROP TABLE public.transactions;
       public         heap    student    false         �            1259    74015    transactions_id_seq    SEQUENCE     �   CREATE SEQUENCE public.transactions_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 *   DROP SEQUENCE public.transactions_id_seq;
       public          student    false    209         �           0    0    transactions_id_seq    SEQUENCE OWNED BY     K   ALTER SEQUENCE public.transactions_id_seq OWNED BY public.transactions.id;
          public          student    false    208         A           2604    73889    cashiers id    DEFAULT     j   ALTER TABLE ONLY public.cashiers ALTER COLUMN id SET DEFAULT nextval('public.cashiers_id_seq'::regclass);
 :   ALTER TABLE public.cashiers ALTER COLUMN id DROP DEFAULT;
       public          student    false    202    203    203         @           2604    73881 
   clients id    DEFAULT     h   ALTER TABLE ONLY public.clients ALTER COLUMN id SET DEFAULT nextval('public.clients_id_seq'::regclass);
 9   ALTER TABLE public.clients ALTER COLUMN id DROP DEFAULT;
       public          student    false    201    200    201         B           2604    73897    currencies id    DEFAULT     n   ALTER TABLE ONLY public.currencies ALTER COLUMN id SET DEFAULT nextval('public.currencies_id_seq'::regclass);
 <   ALTER TABLE public.currencies ALTER COLUMN id DROP DEFAULT;
       public          student    false    204    205    205         C           2604    73905    rates id    DEFAULT     d   ALTER TABLE ONLY public.rates ALTER COLUMN id SET DEFAULT nextval('public.rates_id_seq'::regclass);
 7   ALTER TABLE public.rates ALTER COLUMN id DROP DEFAULT;
       public          student    false    207    206    207         D           2604    74020    transactions id    DEFAULT     r   ALTER TABLE ONLY public.transactions ALTER COLUMN id SET DEFAULT nextval('public.transactions_id_seq'::regclass);
 >   ALTER TABLE public.transactions ALTER COLUMN id DROP DEFAULT;
       public          student    false    208    209    209         �          0    73886    cashiers 
   TABLE DATA           A   COPY public.cashiers (id, name, surname, patronymic) FROM stdin;
    public          student    false    203       3042.dat �          0    73878    clients 
   TABLE DATA           b   COPY public.clients (id, name, surname, patronymic, passport_series, passport_number) FROM stdin;
    public          student    false    201       3040.dat �          0    73894 
   currencies 
   TABLE DATA           4   COPY public.currencies (id, code, name) FROM stdin;
    public          student    false    205       3044.dat �          0    73902    rates 
   TABLE DATA           f   COPY public.rates (id, id_currency_sold, id_currency_purchased, sale_rate, purchase_rate) FROM stdin;
    public          student    false    207       3046.dat �          0    74017    transactions 
   TABLE DATA           �   COPY public.transactions (id, id_currency_sold, id_currency_purchased, id_client, id_cashier, id_rate_sold, id_rate_purchased, date_of_transaction, time_of_transaction, sum_currency_sold, sum_currency_purchased) FROM stdin;
    public          student    false    209       3048.dat �           0    0    cashiers_id_seq    SEQUENCE SET     >   SELECT pg_catalog.setval('public.cashiers_id_seq', 12, true);
          public          student    false    202         �           0    0    clients_id_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('public.clients_id_seq', 12, true);
          public          student    false    200         �           0    0    currencies_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.currencies_id_seq', 3, true);
          public          student    false    204         �           0    0    rates_id_seq    SEQUENCE SET     :   SELECT pg_catalog.setval('public.rates_id_seq', 6, true);
          public          student    false    206         �           0    0    transactions_id_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('public.transactions_id_seq', 11, true);
          public          student    false    208         J           2606    73891    cashiers cashiers_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public.cashiers
    ADD CONSTRAINT cashiers_pkey PRIMARY KEY (id);
 @   ALTER TABLE ONLY public.cashiers DROP CONSTRAINT cashiers_pkey;
       public            student    false    203         H           2606    73883    clients clients_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY public.clients
    ADD CONSTRAINT clients_pkey PRIMARY KEY (id);
 >   ALTER TABLE ONLY public.clients DROP CONSTRAINT clients_pkey;
       public            student    false    201         L           2606    73899    currencies currencies_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public.currencies
    ADD CONSTRAINT currencies_pkey PRIMARY KEY (id);
 D   ALTER TABLE ONLY public.currencies DROP CONSTRAINT currencies_pkey;
       public            student    false    205         N           2606    73910    rates rates_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY public.rates
    ADD CONSTRAINT rates_pkey PRIMARY KEY (id);
 :   ALTER TABLE ONLY public.rates DROP CONSTRAINT rates_pkey;
       public            student    false    207         P           2606    74027    transactions transactions_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public.transactions
    ADD CONSTRAINT transactions_pkey PRIMARY KEY (id);
 H   ALTER TABLE ONLY public.transactions DROP CONSTRAINT transactions_pkey;
       public            student    false    209         Z           2620    73967    cashiers delete_cashiers    TRIGGER     x   CREATE TRIGGER delete_cashiers BEFORE DELETE ON public.cashiers FOR EACH ROW EXECUTE FUNCTION public.delete_cashiers();
 1   DROP TRIGGER delete_cashiers ON public.cashiers;
       public          student    false    210    203         Y           2620    73965    clients delete_clients    TRIGGER     u   CREATE TRIGGER delete_clients BEFORE DELETE ON public.clients FOR EACH ROW EXECUTE FUNCTION public.delete_clients();
 /   DROP TRIGGER delete_clients ON public.clients;
       public          student    false    212    201         [           2620    73969    currencies delete_currencies    TRIGGER     ~   CREATE TRIGGER delete_currencies BEFORE DELETE ON public.currencies FOR EACH ROW EXECUTE FUNCTION public.delete_currencies();
 5   DROP TRIGGER delete_currencies ON public.currencies;
       public          student    false    213    205         \           2620    73971    rates delete_rates    TRIGGER     o   CREATE TRIGGER delete_rates BEFORE DELETE ON public.rates FOR EACH ROW EXECUTE FUNCTION public.delete_rates();
 +   DROP TRIGGER delete_rates ON public.rates;
       public          student    false    207    211         R           2606    73916 &   rates rates_id_currency_purchased_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.rates
    ADD CONSTRAINT rates_id_currency_purchased_fkey FOREIGN KEY (id_currency_purchased) REFERENCES public.currencies(id);
 P   ALTER TABLE ONLY public.rates DROP CONSTRAINT rates_id_currency_purchased_fkey;
       public          student    false    207    205    2892         Q           2606    73911 !   rates rates_id_currency_sold_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.rates
    ADD CONSTRAINT rates_id_currency_sold_fkey FOREIGN KEY (id_currency_sold) REFERENCES public.currencies(id);
 K   ALTER TABLE ONLY public.rates DROP CONSTRAINT rates_id_currency_sold_fkey;
       public          student    false    205    207    2892         V           2606    74043 )   transactions transactions_id_cashier_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.transactions
    ADD CONSTRAINT transactions_id_cashier_fkey FOREIGN KEY (id_cashier) REFERENCES public.cashiers(id);
 S   ALTER TABLE ONLY public.transactions DROP CONSTRAINT transactions_id_cashier_fkey;
       public          student    false    203    209    2890         U           2606    74038 (   transactions transactions_id_client_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.transactions
    ADD CONSTRAINT transactions_id_client_fkey FOREIGN KEY (id_client) REFERENCES public.clients(id);
 R   ALTER TABLE ONLY public.transactions DROP CONSTRAINT transactions_id_client_fkey;
       public          student    false    2888    201    209         T           2606    74033 4   transactions transactions_id_currency_purchased_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.transactions
    ADD CONSTRAINT transactions_id_currency_purchased_fkey FOREIGN KEY (id_currency_purchased) REFERENCES public.currencies(id);
 ^   ALTER TABLE ONLY public.transactions DROP CONSTRAINT transactions_id_currency_purchased_fkey;
       public          student    false    205    209    2892         S           2606    74028 /   transactions transactions_id_currency_sold_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.transactions
    ADD CONSTRAINT transactions_id_currency_sold_fkey FOREIGN KEY (id_currency_sold) REFERENCES public.currencies(id);
 Y   ALTER TABLE ONLY public.transactions DROP CONSTRAINT transactions_id_currency_sold_fkey;
       public          student    false    2892    205    209         X           2606    74053 0   transactions transactions_id_rate_purchased_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.transactions
    ADD CONSTRAINT transactions_id_rate_purchased_fkey FOREIGN KEY (id_rate_purchased) REFERENCES public.rates(id);
 Z   ALTER TABLE ONLY public.transactions DROP CONSTRAINT transactions_id_rate_purchased_fkey;
       public          student    false    209    2894    207         W           2606    74048 +   transactions transactions_id_rate_sold_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.transactions
    ADD CONSTRAINT transactions_id_rate_sold_fkey FOREIGN KEY (id_rate_sold) REFERENCES public.rates(id);
 U   ALTER TABLE ONLY public.transactions DROP CONSTRAINT transactions_id_rate_sold_fkey;
       public          student    false    2894    207    209                                                             3042.dat                                                                                            0000600 0004000 0002000 00000000534 14332211031 0014232 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        1	Daniil	Mostovoy	Vitalevich
2	Aleksandr	Kovalchuk	Vladimirovich
3	Oleg	Ivanov	Ivanovich
4	Sveta	Suslova	Semenovna
5	Daniil	Kovalchuk	Artemovich
6	Artem	Kurilko	Antonovich
7	Anton	Kirpun	Vitalevich
8	Sergey	Savenkov	Artemovich
9	Sergey	Subbota	Vitalevich
10	Vladislav	Smit	Antonovich
11	Maria	Balashova	Vitalevna
12	Dariya	Gorshkova	Semenovna
\.


                                                                                                                                                                    3040.dat                                                                                            0000600 0004000 0002000 00000000754 14332211031 0014234 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        1	Daniil	Mostovoy	Vitalevich	1234	234556
2	Aleksandr	Kovalchuk	Vladimirovich	5454	245656
3	Oleg	Ivanov	Ivanovich	7489	358595
4	Sveta	Suslova	Semenovna	5266	588655
5	Daniil	Kovalchuk	Artemovich	6456	576788
6	Artem	Kurilko	Antonovich	5555	567567
8	Sergey	Savenkov	Artemovich	5775	333335
9	Sergey	Subbota	Vitalevich	8991	199875
10	Vladislav	Smit	Antonovich	2225	478215
11	Maria	Balashova	Vitalevna	4567	452876
7	Anton	Kirpun	Vitalevich	7811	576855
12	Dariya	Gorshkova	Semenovna	4568	782265
\.


                    3044.dat                                                                                            0000600 0004000 0002000 00000000043 14332211031 0014227 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        1	840	USD
2	978	EUR
3	643	RUB
\.


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             3046.dat                                                                                            0000600 0004000 0002000 00000000155 14332211031 0014235 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        1	1	2	1.03	1.23
2	1	3	63.08	69.05
3	2	1	0.97	0.77
4	2	3	75.08	79.08
5	3	1	0.016	0.023
6	3	2	0.023	0.029
\.


                                                                                                                                                                                                                                                                                                                                                                                                                   3048.dat                                                                                            0000600 0004000 0002000 00000001061 14332211031 0014234 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        1	3	1	4	6	5	5	2022-11-06	21:38:02	1300	20.800000
2	3	2	1	8	6	6	2022-11-06	21:40:55	6896.551724	200
3	1	3	9	10	2	2	2022-11-06	21:43:10	23.4	1476.072000
4	2	1	2	3	3	3	2022-11-06	21:45:30	124.34	120.609800
5	2	1	7	9	3	3	2022-11-06	21:48:27	59.428571	45.76
6	1	2	4	11	1	1	2022-11-06	21:59:07	365.853659	450
7	1	3	7	9	2	2	2022-11-06	22:00:30	9.128747	630.34
8	3	2	6	4	6	6	2022-11-06	22:01:59	2689.655172	78
9	3	2	1	2	6	6	2022-11-06	22:03:55	890.34	20.477820
10	2	1	10	12	3	3	2022-11-06	22:05:05	132	128.040000
11	2	1	11	4	3	3	2022-11-06	22:06:58	415.584416	320
\.


                                                                                                                                                                                                                                                                                                                                                                                                                                                                               restore.sql                                                                                         0000600 0004000 0002000 00000033351 14332211031 0015357 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        --
-- NOTE:
--
-- File paths need to be edited. Search for $$PATH$$ and
-- replace it with the path to the directory containing
-- the extracted data files.
--
--
-- PostgreSQL database dump
--

-- Dumped from database version 13.4
-- Dumped by pg_dump version 13.4

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

DROP DATABASE currency_exchanger;
--
-- Name: currency_exchanger; Type: DATABASE; Schema: -; Owner: student
--

CREATE DATABASE currency_exchanger WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'Russian_Russia.1251';


ALTER DATABASE currency_exchanger OWNER TO student;

\connect currency_exchanger

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: delete_cashiers(); Type: FUNCTION; Schema: public; Owner: student
--

CREATE FUNCTION public.delete_cashiers() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
DELETE FROM transactions WHERE transactions.id_cashier = OLD.id;
RETURN OLD;
END;
$$;


ALTER FUNCTION public.delete_cashiers() OWNER TO student;

--
-- Name: delete_clients(); Type: FUNCTION; Schema: public; Owner: student
--

CREATE FUNCTION public.delete_clients() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
DELETE FROM transactions WHERE transactions.id_client = OLD.id;
RETURN OLD;
END;
$$;


ALTER FUNCTION public.delete_clients() OWNER TO student;

--
-- Name: delete_currencies(); Type: FUNCTION; Schema: public; Owner: student
--

CREATE FUNCTION public.delete_currencies() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
DELETE FROM transactions WHERE transactions.id_currency_sold = OLD.id;
DELETE FROM transactions WHERE transactions.id_currency_purchased = OLD.id;
DELETE FROM rates WHERE rates.id_currency_sold = OLD.id;
DELETE FROM rates WHERE rates.id_currency_purchased = OLD.id;
RETURN OLD;
END;
$$;


ALTER FUNCTION public.delete_currencies() OWNER TO student;

--
-- Name: delete_rates(); Type: FUNCTION; Schema: public; Owner: student
--

CREATE FUNCTION public.delete_rates() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
DELETE FROM transactions WHERE transactions.id_rate_sold = OLD.id;
DELETE FROM transactions WHERE transactions.id_rate_purchased = OLD.id;
RETURN OLD;
END;
$$;


ALTER FUNCTION public.delete_rates() OWNER TO student;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: cashiers; Type: TABLE; Schema: public; Owner: student
--

CREATE TABLE public.cashiers (
    id integer NOT NULL,
    name character varying(50) NOT NULL,
    surname character varying(50) NOT NULL,
    patronymic character varying(50) NOT NULL
);


ALTER TABLE public.cashiers OWNER TO student;

--
-- Name: cashiers_id_seq; Type: SEQUENCE; Schema: public; Owner: student
--

CREATE SEQUENCE public.cashiers_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.cashiers_id_seq OWNER TO student;

--
-- Name: cashiers_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: student
--

ALTER SEQUENCE public.cashiers_id_seq OWNED BY public.cashiers.id;


--
-- Name: clients; Type: TABLE; Schema: public; Owner: student
--

CREATE TABLE public.clients (
    id integer NOT NULL,
    name character varying(50) NOT NULL,
    surname character varying(50) NOT NULL,
    patronymic character varying(50) NOT NULL,
    passport_series character varying(4) NOT NULL,
    passport_number character varying(6) NOT NULL
);


ALTER TABLE public.clients OWNER TO student;

--
-- Name: clients_id_seq; Type: SEQUENCE; Schema: public; Owner: student
--

CREATE SEQUENCE public.clients_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.clients_id_seq OWNER TO student;

--
-- Name: clients_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: student
--

ALTER SEQUENCE public.clients_id_seq OWNED BY public.clients.id;


--
-- Name: currencies; Type: TABLE; Schema: public; Owner: student
--

CREATE TABLE public.currencies (
    id integer NOT NULL,
    code character varying(3) NOT NULL,
    name character varying(3) NOT NULL
);


ALTER TABLE public.currencies OWNER TO student;

--
-- Name: currencies_id_seq; Type: SEQUENCE; Schema: public; Owner: student
--

CREATE SEQUENCE public.currencies_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.currencies_id_seq OWNER TO student;

--
-- Name: currencies_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: student
--

ALTER SEQUENCE public.currencies_id_seq OWNED BY public.currencies.id;


--
-- Name: rates; Type: TABLE; Schema: public; Owner: student
--

CREATE TABLE public.rates (
    id integer NOT NULL,
    id_currency_sold integer NOT NULL,
    id_currency_purchased integer NOT NULL,
    sale_rate numeric NOT NULL,
    purchase_rate numeric NOT NULL
);


ALTER TABLE public.rates OWNER TO student;

--
-- Name: rates_id_seq; Type: SEQUENCE; Schema: public; Owner: student
--

CREATE SEQUENCE public.rates_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.rates_id_seq OWNER TO student;

--
-- Name: rates_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: student
--

ALTER SEQUENCE public.rates_id_seq OWNED BY public.rates.id;


--
-- Name: transactions; Type: TABLE; Schema: public; Owner: student
--

CREATE TABLE public.transactions (
    id integer NOT NULL,
    id_currency_sold integer NOT NULL,
    id_currency_purchased integer NOT NULL,
    id_client integer NOT NULL,
    id_cashier integer NOT NULL,
    id_rate_sold integer NOT NULL,
    id_rate_purchased integer NOT NULL,
    date_of_transaction date NOT NULL,
    time_of_transaction time without time zone NOT NULL,
    sum_currency_sold numeric DEFAULT 0,
    sum_currency_purchased numeric DEFAULT 0
);


ALTER TABLE public.transactions OWNER TO student;

--
-- Name: transactions_id_seq; Type: SEQUENCE; Schema: public; Owner: student
--

CREATE SEQUENCE public.transactions_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.transactions_id_seq OWNER TO student;

--
-- Name: transactions_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: student
--

ALTER SEQUENCE public.transactions_id_seq OWNED BY public.transactions.id;


--
-- Name: cashiers id; Type: DEFAULT; Schema: public; Owner: student
--

ALTER TABLE ONLY public.cashiers ALTER COLUMN id SET DEFAULT nextval('public.cashiers_id_seq'::regclass);


--
-- Name: clients id; Type: DEFAULT; Schema: public; Owner: student
--

ALTER TABLE ONLY public.clients ALTER COLUMN id SET DEFAULT nextval('public.clients_id_seq'::regclass);


--
-- Name: currencies id; Type: DEFAULT; Schema: public; Owner: student
--

ALTER TABLE ONLY public.currencies ALTER COLUMN id SET DEFAULT nextval('public.currencies_id_seq'::regclass);


--
-- Name: rates id; Type: DEFAULT; Schema: public; Owner: student
--

ALTER TABLE ONLY public.rates ALTER COLUMN id SET DEFAULT nextval('public.rates_id_seq'::regclass);


--
-- Name: transactions id; Type: DEFAULT; Schema: public; Owner: student
--

ALTER TABLE ONLY public.transactions ALTER COLUMN id SET DEFAULT nextval('public.transactions_id_seq'::regclass);


--
-- Data for Name: cashiers; Type: TABLE DATA; Schema: public; Owner: student
--

COPY public.cashiers (id, name, surname, patronymic) FROM stdin;
\.
COPY public.cashiers (id, name, surname, patronymic) FROM '$$PATH$$/3042.dat';

--
-- Data for Name: clients; Type: TABLE DATA; Schema: public; Owner: student
--

COPY public.clients (id, name, surname, patronymic, passport_series, passport_number) FROM stdin;
\.
COPY public.clients (id, name, surname, patronymic, passport_series, passport_number) FROM '$$PATH$$/3040.dat';

--
-- Data for Name: currencies; Type: TABLE DATA; Schema: public; Owner: student
--

COPY public.currencies (id, code, name) FROM stdin;
\.
COPY public.currencies (id, code, name) FROM '$$PATH$$/3044.dat';

--
-- Data for Name: rates; Type: TABLE DATA; Schema: public; Owner: student
--

COPY public.rates (id, id_currency_sold, id_currency_purchased, sale_rate, purchase_rate) FROM stdin;
\.
COPY public.rates (id, id_currency_sold, id_currency_purchased, sale_rate, purchase_rate) FROM '$$PATH$$/3046.dat';

--
-- Data for Name: transactions; Type: TABLE DATA; Schema: public; Owner: student
--

COPY public.transactions (id, id_currency_sold, id_currency_purchased, id_client, id_cashier, id_rate_sold, id_rate_purchased, date_of_transaction, time_of_transaction, sum_currency_sold, sum_currency_purchased) FROM stdin;
\.
COPY public.transactions (id, id_currency_sold, id_currency_purchased, id_client, id_cashier, id_rate_sold, id_rate_purchased, date_of_transaction, time_of_transaction, sum_currency_sold, sum_currency_purchased) FROM '$$PATH$$/3048.dat';

--
-- Name: cashiers_id_seq; Type: SEQUENCE SET; Schema: public; Owner: student
--

SELECT pg_catalog.setval('public.cashiers_id_seq', 12, true);


--
-- Name: clients_id_seq; Type: SEQUENCE SET; Schema: public; Owner: student
--

SELECT pg_catalog.setval('public.clients_id_seq', 12, true);


--
-- Name: currencies_id_seq; Type: SEQUENCE SET; Schema: public; Owner: student
--

SELECT pg_catalog.setval('public.currencies_id_seq', 3, true);


--
-- Name: rates_id_seq; Type: SEQUENCE SET; Schema: public; Owner: student
--

SELECT pg_catalog.setval('public.rates_id_seq', 6, true);


--
-- Name: transactions_id_seq; Type: SEQUENCE SET; Schema: public; Owner: student
--

SELECT pg_catalog.setval('public.transactions_id_seq', 11, true);


--
-- Name: cashiers cashiers_pkey; Type: CONSTRAINT; Schema: public; Owner: student
--

ALTER TABLE ONLY public.cashiers
    ADD CONSTRAINT cashiers_pkey PRIMARY KEY (id);


--
-- Name: clients clients_pkey; Type: CONSTRAINT; Schema: public; Owner: student
--

ALTER TABLE ONLY public.clients
    ADD CONSTRAINT clients_pkey PRIMARY KEY (id);


--
-- Name: currencies currencies_pkey; Type: CONSTRAINT; Schema: public; Owner: student
--

ALTER TABLE ONLY public.currencies
    ADD CONSTRAINT currencies_pkey PRIMARY KEY (id);


--
-- Name: rates rates_pkey; Type: CONSTRAINT; Schema: public; Owner: student
--

ALTER TABLE ONLY public.rates
    ADD CONSTRAINT rates_pkey PRIMARY KEY (id);


--
-- Name: transactions transactions_pkey; Type: CONSTRAINT; Schema: public; Owner: student
--

ALTER TABLE ONLY public.transactions
    ADD CONSTRAINT transactions_pkey PRIMARY KEY (id);


--
-- Name: cashiers delete_cashiers; Type: TRIGGER; Schema: public; Owner: student
--

CREATE TRIGGER delete_cashiers BEFORE DELETE ON public.cashiers FOR EACH ROW EXECUTE FUNCTION public.delete_cashiers();


--
-- Name: clients delete_clients; Type: TRIGGER; Schema: public; Owner: student
--

CREATE TRIGGER delete_clients BEFORE DELETE ON public.clients FOR EACH ROW EXECUTE FUNCTION public.delete_clients();


--
-- Name: currencies delete_currencies; Type: TRIGGER; Schema: public; Owner: student
--

CREATE TRIGGER delete_currencies BEFORE DELETE ON public.currencies FOR EACH ROW EXECUTE FUNCTION public.delete_currencies();


--
-- Name: rates delete_rates; Type: TRIGGER; Schema: public; Owner: student
--

CREATE TRIGGER delete_rates BEFORE DELETE ON public.rates FOR EACH ROW EXECUTE FUNCTION public.delete_rates();


--
-- Name: rates rates_id_currency_purchased_fkey; Type: FK CONSTRAINT; Schema: public; Owner: student
--

ALTER TABLE ONLY public.rates
    ADD CONSTRAINT rates_id_currency_purchased_fkey FOREIGN KEY (id_currency_purchased) REFERENCES public.currencies(id);


--
-- Name: rates rates_id_currency_sold_fkey; Type: FK CONSTRAINT; Schema: public; Owner: student
--

ALTER TABLE ONLY public.rates
    ADD CONSTRAINT rates_id_currency_sold_fkey FOREIGN KEY (id_currency_sold) REFERENCES public.currencies(id);


--
-- Name: transactions transactions_id_cashier_fkey; Type: FK CONSTRAINT; Schema: public; Owner: student
--

ALTER TABLE ONLY public.transactions
    ADD CONSTRAINT transactions_id_cashier_fkey FOREIGN KEY (id_cashier) REFERENCES public.cashiers(id);


--
-- Name: transactions transactions_id_client_fkey; Type: FK CONSTRAINT; Schema: public; Owner: student
--

ALTER TABLE ONLY public.transactions
    ADD CONSTRAINT transactions_id_client_fkey FOREIGN KEY (id_client) REFERENCES public.clients(id);


--
-- Name: transactions transactions_id_currency_purchased_fkey; Type: FK CONSTRAINT; Schema: public; Owner: student
--

ALTER TABLE ONLY public.transactions
    ADD CONSTRAINT transactions_id_currency_purchased_fkey FOREIGN KEY (id_currency_purchased) REFERENCES public.currencies(id);


--
-- Name: transactions transactions_id_currency_sold_fkey; Type: FK CONSTRAINT; Schema: public; Owner: student
--

ALTER TABLE ONLY public.transactions
    ADD CONSTRAINT transactions_id_currency_sold_fkey FOREIGN KEY (id_currency_sold) REFERENCES public.currencies(id);


--
-- Name: transactions transactions_id_rate_purchased_fkey; Type: FK CONSTRAINT; Schema: public; Owner: student
--

ALTER TABLE ONLY public.transactions
    ADD CONSTRAINT transactions_id_rate_purchased_fkey FOREIGN KEY (id_rate_purchased) REFERENCES public.rates(id);


--
-- Name: transactions transactions_id_rate_sold_fkey; Type: FK CONSTRAINT; Schema: public; Owner: student
--

ALTER TABLE ONLY public.transactions
    ADD CONSTRAINT transactions_id_rate_sold_fkey FOREIGN KEY (id_rate_sold) REFERENCES public.rates(id);


--
-- PostgreSQL database dump complete
--

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       