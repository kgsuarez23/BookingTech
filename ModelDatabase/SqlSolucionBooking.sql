USE [master]
GO

/****** Creacion de base de datos ******/

CREATE DATABASE [DB_Hotel]

/****** Creacion de base de datos ******/

-------------------------------------------------------------------------------------------

USE [DB_Hotel]
GO
/****** Object:  User [usr_booking_tech]    Script Date: 24/02/2025 4:08:24 PM ******/
CREATE USER [usr_booking_tech] FOR LOGIN [usr_booking_tech] WITH DEFAULT_SCHEMA=[db_owner]
GO
ALTER ROLE [db_owner] ADD MEMBER [usr_booking_tech]
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[ReservationID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[CheckInDate] [date] NOT NULL,
	[CheckOutDate] [date] NOT NULL,
	[NumberOfGuests] [int] NOT NULL,
	[ReservationStatus] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ReservationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookingRoom]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookingRoom](
	[ReservationID] [int] NOT NULL,
	[RoomID] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_BookingRoom] PRIMARY KEY CLUSTERED 
(
	[ReservationID] ASC,
	[RoomID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmergencyContact]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmergencyContact](
	[ContactID] [int] IDENTITY(1,1) NOT NULL,
	[ReservationID] [int] NOT NULL,
	[FirstName] [varchar](200) NOT NULL,
	[LastName] [varchar](200) NOT NULL,
	[ContactPhone] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ContactID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Guest]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Guest](
	[GuestID] [int] IDENTITY(1,1) NOT NULL,
	[ReservationID] [int] NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[BirthDate] [date] NOT NULL,
	[Gender] [varchar](10) NOT NULL,
	[DocumentType] [varchar](20) NOT NULL,
	[DocumentNumber] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[ContactPhone] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[GuestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Hotel]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hotel](
	[HotelID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Address] [varchar](200) NOT NULL,
	[City] [varchar](50) NOT NULL,
	[State] [varchar](50) NOT NULL,
	[Country] [varchar](50) NOT NULL,
	[Phone] [varchar](20) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[HotelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](50) NOT NULL,
	[Description] [varchar](255) NULL,
	[IsActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Room]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Room](
	[RoomID] [int] IDENTITY(1,1) NOT NULL,
	[HotelID] [int] NOT NULL,
	[TypeID] [int] NOT NULL,
	[Number] [varchar](10) NOT NULL,
	[BaseCost] [decimal](10, 2) NOT NULL,
	[Taxes] [decimal](5, 2) NOT NULL,
	[Location] [varchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoomID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoomType]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomType](
	[TypeID] [int] IDENTITY(1,1) NOT NULL,
	[NameType] [varchar](100) NOT NULL,
	[NumberOfGuests] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[PasswordHash] [varchar](255) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[RegistrationDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[UserID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
	[IsActive] [bit] NOT NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Booking] ON 
GO
INSERT [dbo].[Booking] ([ReservationID], [UserID], [CheckInDate], [CheckOutDate], [NumberOfGuests], [ReservationStatus]) VALUES (1, 1, CAST(N'2022-01-01' AS Date), CAST(N'2022-01-15' AS Date), 2, 1)
GO
INSERT [dbo].[Booking] ([ReservationID], [UserID], [CheckInDate], [CheckOutDate], [NumberOfGuests], [ReservationStatus]) VALUES (2, 1, CAST(N'2022-02-01' AS Date), CAST(N'2022-02-10' AS Date), 1, 1)
GO
INSERT [dbo].[Booking] ([ReservationID], [UserID], [CheckInDate], [CheckOutDate], [NumberOfGuests], [ReservationStatus]) VALUES (3, 1, CAST(N'2022-03-01' AS Date), CAST(N'2022-03-05' AS Date), 3, 1)
GO
INSERT [dbo].[Booking] ([ReservationID], [UserID], [CheckInDate], [CheckOutDate], [NumberOfGuests], [ReservationStatus]) VALUES (4, 1, CAST(N'2022-12-20' AS Date), CAST(N'2022-12-31' AS Date), 1, 1)
GO
INSERT [dbo].[Booking] ([ReservationID], [UserID], [CheckInDate], [CheckOutDate], [NumberOfGuests], [ReservationStatus]) VALUES (5, 1, CAST(N'2022-01-01' AS Date), CAST(N'2022-01-10' AS Date), 1, 1)
GO
INSERT [dbo].[Booking] ([ReservationID], [UserID], [CheckInDate], [CheckOutDate], [NumberOfGuests], [ReservationStatus]) VALUES (6, 1, CAST(N'2022-02-01' AS Date), CAST(N'2022-02-05' AS Date), 1, 1)
GO
INSERT [dbo].[Booking] ([ReservationID], [UserID], [CheckInDate], [CheckOutDate], [NumberOfGuests], [ReservationStatus]) VALUES (7, 1, CAST(N'2022-04-01' AS Date), CAST(N'2022-06-08' AS Date), 2, 1)
GO
INSERT [dbo].[Booking] ([ReservationID], [UserID], [CheckInDate], [CheckOutDate], [NumberOfGuests], [ReservationStatus]) VALUES (8, 1, CAST(N'2025-02-25' AS Date), CAST(N'2025-02-26' AS Date), 3, 1)
GO
INSERT [dbo].[Booking] ([ReservationID], [UserID], [CheckInDate], [CheckOutDate], [NumberOfGuests], [ReservationStatus]) VALUES (12, 1, CAST(N'2025-03-25' AS Date), CAST(N'2025-03-28' AS Date), 1, NULL)
GO
SET IDENTITY_INSERT [dbo].[Booking] OFF
GO
INSERT [dbo].[BookingRoom] ([ReservationID], [RoomID], [IsActive]) VALUES (1, 1, 1)
GO
INSERT [dbo].[BookingRoom] ([ReservationID], [RoomID], [IsActive]) VALUES (2, 1, 1)
GO
INSERT [dbo].[BookingRoom] ([ReservationID], [RoomID], [IsActive]) VALUES (3, 1, 1)
GO
INSERT [dbo].[BookingRoom] ([ReservationID], [RoomID], [IsActive]) VALUES (4, 2, 1)
GO
INSERT [dbo].[BookingRoom] ([ReservationID], [RoomID], [IsActive]) VALUES (5, 3, 1)
GO
INSERT [dbo].[BookingRoom] ([ReservationID], [RoomID], [IsActive]) VALUES (6, 4, 1)
GO
INSERT [dbo].[BookingRoom] ([ReservationID], [RoomID], [IsActive]) VALUES (6, 5, 1)
GO
INSERT [dbo].[BookingRoom] ([ReservationID], [RoomID], [IsActive]) VALUES (7, 7, 1)
GO
INSERT [dbo].[BookingRoom] ([ReservationID], [RoomID], [IsActive]) VALUES (8, 1, 1)
GO
INSERT [dbo].[BookingRoom] ([ReservationID], [RoomID], [IsActive]) VALUES (8, 2, 1)
GO
INSERT [dbo].[BookingRoom] ([ReservationID], [RoomID], [IsActive]) VALUES (12, 6, 1)
GO
SET IDENTITY_INSERT [dbo].[EmergencyContact] ON 
GO
INSERT [dbo].[EmergencyContact] ([ContactID], [ReservationID], [FirstName], [LastName], [ContactPhone]) VALUES (1, 1, N'Jesus', N'Suarez', N'+573102516391')
GO
INSERT [dbo].[EmergencyContact] ([ContactID], [ReservationID], [FirstName], [LastName], [ContactPhone]) VALUES (2, 2, N'Rosa', N'Pineda', N'+573102516392')
GO
INSERT [dbo].[EmergencyContact] ([ContactID], [ReservationID], [FirstName], [LastName], [ContactPhone]) VALUES (3, 3, N'German', N'Suarez', N'+573102516393')
GO
INSERT [dbo].[EmergencyContact] ([ContactID], [ReservationID], [FirstName], [LastName], [ContactPhone]) VALUES (4, 4, N'Monica', N'Vargas', N'+573102516394')
GO
INSERT [dbo].[EmergencyContact] ([ContactID], [ReservationID], [FirstName], [LastName], [ContactPhone]) VALUES (5, 5, N'Juan', N'Garcia', N'+573102516395')
GO
INSERT [dbo].[EmergencyContact] ([ContactID], [ReservationID], [FirstName], [LastName], [ContactPhone]) VALUES (6, 6, N'Silvia', N'Alvarado', N'+573102516397')
GO
INSERT [dbo].[EmergencyContact] ([ContactID], [ReservationID], [FirstName], [LastName], [ContactPhone]) VALUES (7, 7, N'Jose', N'Arguello', N'+573102516398')
GO
INSERT [dbo].[EmergencyContact] ([ContactID], [ReservationID], [FirstName], [LastName], [ContactPhone]) VALUES (8, 8, N'Ana', N'Sánchez', N'+573102516399')
GO
INSERT [dbo].[EmergencyContact] ([ContactID], [ReservationID], [FirstName], [LastName], [ContactPhone]) VALUES (9, 12, N'Alvaro', N'Gomez', N'+573102516396')
GO
SET IDENTITY_INSERT [dbo].[EmergencyContact] OFF
GO
SET IDENTITY_INSERT [dbo].[Guest] ON 
GO
INSERT [dbo].[Guest] ([GuestID], [ReservationID], [FirstName], [LastName], [BirthDate], [Gender], [DocumentType], [DocumentNumber], [Email], [ContactPhone]) VALUES (1, 1, N'Kevin', N'Suarez', CAST(N'1995-07-23' AS Date), N'M', N'CC', N'123456', N'ksgiovanny@gmail.com', N'+573102516396')
GO
INSERT [dbo].[Guest] ([GuestID], [ReservationID], [FirstName], [LastName], [BirthDate], [Gender], [DocumentType], [DocumentNumber], [Email], [ContactPhone]) VALUES (2, 1, N'Monica', N'Vargas', CAST(N'1997-05-09' AS Date), N'F', N'CC', N'123456', N'monicavargasortiz97@gmail.com', N'+573102516396')
GO
INSERT [dbo].[Guest] ([GuestID], [ReservationID], [FirstName], [LastName], [BirthDate], [Gender], [DocumentType], [DocumentNumber], [Email], [ContactPhone]) VALUES (3, 2, N'Jesus', N'Suarez', CAST(N'1970-12-18' AS Date), N'M', N'CC', N'123456', N'ksgiovanny@gmail.com', N'+573102516396')
GO
INSERT [dbo].[Guest] ([GuestID], [ReservationID], [FirstName], [LastName], [BirthDate], [Gender], [DocumentType], [DocumentNumber], [Email], [ContactPhone]) VALUES (4, 3, N'Rosa', N'Pineda', CAST(N'1967-07-23' AS Date), N'F', N'CC', N'123456', N'ksgiovanny@gmail.com', N'+573102516396')
GO
INSERT [dbo].[Guest] ([GuestID], [ReservationID], [FirstName], [LastName], [BirthDate], [Gender], [DocumentType], [DocumentNumber], [Email], [ContactPhone]) VALUES (5, 4, N'German', N'Suarez', CAST(N'1997-03-16' AS Date), N'M', N'CC', N'123456', N'ksgiovanny@gmail.com', N'+573102516396')
GO
INSERT [dbo].[Guest] ([GuestID], [ReservationID], [FirstName], [LastName], [BirthDate], [Gender], [DocumentType], [DocumentNumber], [Email], [ContactPhone]) VALUES (6, 5, N'Juan', N'Garcia', CAST(N'1990-04-13' AS Date), N'M', N'CC', N'123456', N'ksgiovanny@gmail.com', N'+573102516396')
GO
INSERT [dbo].[Guest] ([GuestID], [ReservationID], [FirstName], [LastName], [BirthDate], [Gender], [DocumentType], [DocumentNumber], [Email], [ContactPhone]) VALUES (7, 6, N'Silvia', N'Alvarado', CAST(N'1983-07-15' AS Date), N'F', N'CC', N'123456', N'ksgiovanny@gmail.com', N'+573102516396')
GO
INSERT [dbo].[Guest] ([GuestID], [ReservationID], [FirstName], [LastName], [BirthDate], [Gender], [DocumentType], [DocumentNumber], [Email], [ContactPhone]) VALUES (8, 7, N'Jose', N'Arguello', CAST(N'1976-08-21' AS Date), N'M', N'CC', N'123456', N'ksgiovanny@gmail.com', N'+573102516396')
GO
INSERT [dbo].[Guest] ([GuestID], [ReservationID], [FirstName], [LastName], [BirthDate], [Gender], [DocumentType], [DocumentNumber], [Email], [ContactPhone]) VALUES (9, 8, N'Carlos', N'Ramírez', CAST(N'1980-01-15' AS Date), N'M', N'CC', N'123456', N'ksgiovanny@gmail.com', N'+573102516396')
GO
INSERT [dbo].[Guest] ([GuestID], [ReservationID], [FirstName], [LastName], [BirthDate], [Gender], [DocumentType], [DocumentNumber], [Email], [ContactPhone]) VALUES (10, 8, N'Lucía', N'Martínez', CAST(N'1990-05-10' AS Date), N'F', N'CC', N'123456', N'ksgiovanny@gmail.com', N'+573102516396')
GO
INSERT [dbo].[Guest] ([GuestID], [ReservationID], [FirstName], [LastName], [BirthDate], [Gender], [DocumentType], [DocumentNumber], [Email], [ContactPhone]) VALUES (11, 8, N'Marta', N'González', CAST(N'1985-09-25' AS Date), N'F', N'CC', N'123456', N'ksgiovanny@gmail.com', N'+573102516396')
GO
INSERT [dbo].[Guest] ([GuestID], [ReservationID], [FirstName], [LastName], [BirthDate], [Gender], [DocumentType], [DocumentNumber], [Email], [ContactPhone]) VALUES (12, 12, N'Pablo', N'Blanco', CAST(N'1987-07-15' AS Date), N'M', N'CC', N'123456', N'pablo.blanco@example.com', N'+573102516396')
GO
SET IDENTITY_INSERT [dbo].[Guest] OFF
GO
SET IDENTITY_INSERT [dbo].[Hotel] ON 
GO
INSERT [dbo].[Hotel] ([HotelID], [Name], [Address], [City], [State], [Country], [Phone], [Email], [IsActive]) VALUES (1, N'Holiday Inn Bucaramanga Cacique', N'Transversal Oriental con calle 93, Sotomayor', N'Bucaramanga', N'Santander', N'Colombia', N'(607)6917300', N'hotel1@gmail.com', 1)
GO
INSERT [dbo].[Hotel] ([HotelID], [Name], [Address], [City], [State], [Country], [Phone], [Email], [IsActive]) VALUES (2, N'Hampton by Hilton', N'Cra. 33 #46-07, Cabecera del llano', N'Bucaramanga', N'Santander', N'Colombia', N'(607) 6973535', N'hotel2@gmail.com', 1)
GO
INSERT [dbo].[Hotel] ([HotelID], [Name], [Address], [City], [State], [Country], [Phone], [Email], [IsActive]) VALUES (3, N'Hotel Dann Carlton', N'Cl. 47 #n 28-83, Sotomayor', N'Bucaramanga', N'Santander', N'Colombia', N'(607)6431919', N'hotel3@gmail.com', 1)
GO
INSERT [dbo].[Hotel] ([HotelID], [Name], [Address], [City], [State], [Country], [Phone], [Email], [IsActive]) VALUES (4, N'Sonesta Hotel', N'Parque Caracolí, Tv. El Bosque #29 – 145 C.C', N'Floridablanca', N'Santander', N'Colombia', N'(607)6186818', N'hotel4@gmail.com', 1)
GO
INSERT [dbo].[Hotel] ([HotelID], [Name], [Address], [City], [State], [Country], [Phone], [Email], [IsActive]) VALUES (5, N'Hotel Roseliere', N'Cl. 30 #24-38', N'Floridablanca', N'Santander', N'Colombia', N'+573004747694', N'hotel5@gmail.com', 1)
GO
INSERT [dbo].[Hotel] ([HotelID], [Name], [Address], [City], [State], [Country], [Phone], [Email], [IsActive]) VALUES (6, N'Hotel Girón Plaza', N'Cl. 43 # 22-52', N'Girón', N'Santander', N'Colombia', N'+573156254021', N'hotel6@gmail.com', 1)
GO
INSERT [dbo].[Hotel] ([HotelID], [Name], [Address], [City], [State], [Country], [Phone], [Email], [IsActive]) VALUES (7, N'Hotel Giron Campestre', N'Autopista Aeropuerto km 13', N'Girón', N'Santander', N'Colombia', N'+573152800231', N'hotel7@gmail.com', 1)
GO
INSERT [dbo].[Hotel] ([HotelID], [Name], [Address], [City], [State], [Country], [Phone], [Email], [IsActive]) VALUES (8, N'Hostel Nirvana San Gil', N'Cra. 12 #4-34', N'San Gil', N'Santander', N'Colombia', N'+573206456384', N'hotel8@gmail.com', 1)
GO
SET IDENTITY_INSERT [dbo].[Hotel] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 
GO
INSERT [dbo].[Role] ([RoleID], [RoleName], [Description], [IsActive]) VALUES (1, N'Administrador', N'Rol de Administrador', 1)
GO
INSERT [dbo].[Role] ([RoleID], [RoleName], [Description], [IsActive]) VALUES (2, N'Usuario', N'Rol de Usuario', 1)
GO
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Room] ON 
GO
INSERT [dbo].[Room] ([RoomID], [HotelID], [TypeID], [Number], [BaseCost], [Taxes], [Location], [IsActive]) VALUES (1, 1, 2, N'1A', CAST(10.00 AS Decimal(10, 2)), CAST(2.30 AS Decimal(5, 2)), N'Ala norte', 1)
GO
INSERT [dbo].[Room] ([RoomID], [HotelID], [TypeID], [Number], [BaseCost], [Taxes], [Location], [IsActive]) VALUES (2, 1, 1, N'1B', CAST(5.00 AS Decimal(10, 2)), CAST(1.00 AS Decimal(5, 2)), N'Ala norte', 1)
GO
INSERT [dbo].[Room] ([RoomID], [HotelID], [TypeID], [Number], [BaseCost], [Taxes], [Location], [IsActive]) VALUES (3, 3, 2, N'1A', CAST(11.00 AS Decimal(10, 2)), CAST(2.40 AS Decimal(5, 2)), N'Ala oeste', 1)
GO
INSERT [dbo].[Room] ([RoomID], [HotelID], [TypeID], [Number], [BaseCost], [Taxes], [Location], [IsActive]) VALUES (4, 3, 1, N'1B', CAST(6.00 AS Decimal(10, 2)), CAST(1.10 AS Decimal(5, 2)), N'Ala oeste', 1)
GO
INSERT [dbo].[Room] ([RoomID], [HotelID], [TypeID], [Number], [BaseCost], [Taxes], [Location], [IsActive]) VALUES (5, 2, 3, N'3A', CAST(15.00 AS Decimal(10, 2)), CAST(3.00 AS Decimal(5, 2)), N'Ala este', 1)
GO
INSERT [dbo].[Room] ([RoomID], [HotelID], [TypeID], [Number], [BaseCost], [Taxes], [Location], [IsActive]) VALUES (6, 2, 2, N'5C', CAST(10.00 AS Decimal(10, 2)), CAST(2.00 AS Decimal(5, 2)), N'Ala este', 1)
GO
INSERT [dbo].[Room] ([RoomID], [HotelID], [TypeID], [Number], [BaseCost], [Taxes], [Location], [IsActive]) VALUES (7, 4, 1, N'2A', CAST(5.00 AS Decimal(10, 2)), CAST(4.00 AS Decimal(5, 2)), N'Ala sur', 1)
GO
INSERT [dbo].[Room] ([RoomID], [HotelID], [TypeID], [Number], [BaseCost], [Taxes], [Location], [IsActive]) VALUES (8, 1, 2, N'4C', CAST(12.00 AS Decimal(10, 2)), CAST(2.10 AS Decimal(5, 2)), N'Cuarto piso', 1)
GO
INSERT [dbo].[Room] ([RoomID], [HotelID], [TypeID], [Number], [BaseCost], [Taxes], [Location], [IsActive]) VALUES (9, 5, 3, N'1C', CAST(5.00 AS Decimal(10, 2)), CAST(4.00 AS Decimal(5, 2)), N'Ala norte', 1)
GO
INSERT [dbo].[Room] ([RoomID], [HotelID], [TypeID], [Number], [BaseCost], [Taxes], [Location], [IsActive]) VALUES (10, 8, 2, N'1C', CAST(20.00 AS Decimal(10, 2)), CAST(1.00 AS Decimal(5, 2)), N'Ala sur', 1)
GO
SET IDENTITY_INSERT [dbo].[Room] OFF
GO
SET IDENTITY_INSERT [dbo].[RoomType] ON 
GO
INSERT [dbo].[RoomType] ([TypeID], [NameType], [NumberOfGuests]) VALUES (1, N'Individual', 1)
GO
INSERT [dbo].[RoomType] ([TypeID], [NameType], [NumberOfGuests]) VALUES (2, N'Double', 2)
GO
INSERT [dbo].[RoomType] ([TypeID], [NameType], [NumberOfGuests]) VALUES (3, N'Triple', 3)
GO
INSERT [dbo].[RoomType] ([TypeID], [NameType], [NumberOfGuests]) VALUES (4, N'Quadruple', 4)
GO
SET IDENTITY_INSERT [dbo].[RoomType] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([UserID], [Username], [PasswordHash], [Email], [FirstName], [LastName], [RegistrationDate], [IsActive]) VALUES (1, N'k.suarez', N'AQAAAAIAAYagAAAAEKfv5+BBibLyE1o8QDfwFNn1X9XFtoCenI3pvfYzyCGL3lQ0W3AHKVVQur8PkIce1Q==', N'ksgiovanny@gmail.com', N'Kevin', N'Suarez', CAST(N'2025-02-22T22:56:21.700' AS DateTime), 1)
GO
INSERT [dbo].[User] ([UserID], [Username], [PasswordHash], [Email], [FirstName], [LastName], [RegistrationDate], [IsActive]) VALUES (2, N'm.vargas', N'AQAAAAIAAYagAAAAEF2a2xQY19tk1k6F8dqJesxdXPxqDIxfvcWLjEjHLJd8V7xM5PdZ+S2hkIbKhwD/SA==', N'monicavargasortiz97@gmail.com', N'Monica', N'Vargas', CAST(N'2025-02-25T10:27:07.017' AS DateTime), 1)
GO
INSERT [dbo].[User] ([UserID], [Username], [PasswordHash], [Email], [FirstName], [LastName], [RegistrationDate], [IsActive]) VALUES (4, N'g.suarez', N'AQAAAAIAAC7gAAAAEK1ezzgG1Sabhc240R3OzMfN8dxraE2QEKTznPX3v0n83E+fiv+3UpmHn/HeCG4wBg==', N'germandario97@gmail.com', N'German', N'Suarez', CAST(N'2025-02-26T14:59:44.837' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
INSERT [dbo].[UserRole] ([UserID], [RoleID], [IsActive]) VALUES (1, 1, 1)
GO
INSERT [dbo].[UserRole] ([UserID], [RoleID], [IsActive]) VALUES (1, 2, 1)
GO
INSERT [dbo].[UserRole] ([UserID], [RoleID], [IsActive]) VALUES (2, 1, 1)
GO
INSERT [dbo].[UserRole] ([UserID], [RoleID], [IsActive]) VALUES (4, 1, 1)
GO
ALTER TABLE [dbo].[BookingRoom] ADD  DEFAULT ((1)) FOR [IsActive]
GO
/****** Object:  StoredProcedure [dbo].[BOOKING_GET_ALL]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author, KEVIN SUAREZ>
-- Create date: <Create Date, 22/02/2025>
-- Description:	<Description, Listar todas las reservas>
-- =============================================
CREATE PROCEDURE [dbo].[BOOKING_GET_ALL]
    @OUT_DATA NVARCHAR(MAX) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT @OUT_DATA = (SELECT 
    b.ReservationID,
    b.CheckInDate,
    b.CheckOutDate,
    b.NumberOfGuests,
    b.ReservationStatus,
    
    -- Información del hotel (se recupera con CROSS APPLY para evitar duplicados)
    h.HotelID,
    h.Name AS HotelName,
    h.Address AS HotelAddress,
    h.Country AS HotelCountry,
    h.State AS HotelState,
    h.City AS HotelCity,
    h.Phone AS HotelPhone,
    h.Email AS HotelEmail,
    h.IsActive AS HotelIsActive,
    
    -- Subconsulta para obtener las habitaciones en formato JSON
    JSON_QUERY((
        SELECT 
            r.RoomID,
            r.Number,
            JSON_QUERY((
                SELECT 
                    rt.TypeID,
                    rt.NameType,
                    rt.NumberOfGuests
                FROM [dbo].[RoomType] rt
                WHERE rt.TypeID = r.TypeID
                FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
            )) AS TypeRoom,  -- Objeto JSON con la info del tipo de habitación
            r.BaseCost,
            r.Taxes,
            r.Location,
            r.IsActive,
            br2.IsActive AS BookingRoomIsActive
        FROM [dbo].[BookingRoom] br2
        JOIN [dbo].[Room] r ON br2.RoomID = r.RoomID
        WHERE br2.ReservationID = b.ReservationID
        FOR JSON PATH
    )) AS Rooms,
    
    -- Subconsulta para obtener los huéspedes en formato JSON
    JSON_QUERY((
        SELECT 
            g.GuestID,
            g.FirstName,
            g.LastName,
            g.BirthDate,
            g.Gender,
            g.DocumentType,
            g.DocumentNumber,
            g.Email,
            g.ContactPhone
        FROM [dbo].[Guest] g
        WHERE g.ReservationID = b.ReservationID
        FOR JSON PATH
    )) AS Guests,
    
    -- Subconsulta para obtener el contacto de emergencia en formato JSON
    JSON_QUERY((
        SELECT 
            ec.ContactID,
            ec.ReservationID,
            ec.FirstName,
            ec.LastName,
            ec.ContactPhone
        FROM [dbo].[EmergencyContact] ec
        WHERE ec.ReservationID = b.ReservationID
        FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
    )) AS EmergencyContact

FROM [dbo].[Booking] b
CROSS APPLY (
    SELECT TOP 1
        h.HotelID,
        h.Name,
        h.Address,
        h.Country,
        h.State,
        h.City,
        h.Phone,
        h.Email,
        h.IsActive
    FROM [dbo].[BookingRoom] br2
    JOIN [dbo].[Room] r ON br2.RoomID = r.RoomID
    JOIN [dbo].[Hotel] h ON r.HotelID = h.HotelID
    WHERE br2.ReservationID = b.ReservationID
) h
ORDER BY b.ReservationID
		FOR JSON PATH
		);
END
GO
/****** Object:  StoredProcedure [dbo].[BOOKING_GETBY_ID]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author, KEVIN SUAREZ>
-- Create date: <Create Date, 22/02/2025>
-- Description:	<Description, Listar todas las reservas>
-- =============================================
CREATE PROCEDURE [dbo].[BOOKING_GETBY_ID]
    @OUT_DATA NVARCHAR(MAX) OUTPUT,
    @RESERVATION_ID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT @OUT_DATA = (SELECT 
    b.ReservationID,
    b.CheckInDate,
    b.CheckOutDate,
    b.NumberOfGuests,
    b.ReservationStatus,
    
    -- Información del hotel (se recupera con CROSS APPLY para evitar duplicados)
    h.HotelID,
    h.Name AS HotelName,
    h.Address AS HotelAddress,
    h.Country AS HotelCountry,
    h.State AS HotelState,
    h.City AS HotelCity,
    h.Phone AS HotelPhone,
    h.Email AS HotelEmail,
    h.IsActive AS HotelIsActive,
    
    -- Subconsulta para obtener las habitaciones en formato JSON
    JSON_QUERY((
        SELECT 
            r.RoomID,
            r.Number,
            JSON_QUERY((
                SELECT 
                    rt.TypeID,
                    rt.NameType,
                    rt.NumberOfGuests
                FROM [dbo].[RoomType] rt
                WHERE rt.TypeID = r.TypeID
                FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
            )) AS TypeRoom,  -- Objeto JSON con la info del tipo de habitación
            r.BaseCost,
            r.Taxes,
            r.Location,
            r.IsActive,
            br2.IsActive AS BookingRoomIsActive
        FROM [dbo].[BookingRoom] br2
        JOIN [dbo].[Room] r ON br2.RoomID = r.RoomID
        WHERE br2.ReservationID = b.ReservationID
        FOR JSON PATH
    )) AS Rooms,
    
    -- Subconsulta para obtener los huéspedes en formato JSON
    JSON_QUERY((
        SELECT 
            g.GuestID,
            g.FirstName,
            g.LastName,
            g.BirthDate,
            g.Gender,
            g.DocumentType,
            g.DocumentNumber,
            g.Email,
            g.ContactPhone
        FROM [dbo].[Guest] g
        WHERE g.ReservationID = b.ReservationID
        FOR JSON PATH
    )) AS Guests,
    
    -- Subconsulta para obtener el contacto de emergencia en formato JSON
    JSON_QUERY((
        SELECT 
            ec.ContactID,
            ec.ReservationID,
            ec.FirstName,
            ec.LastName,
            ec.ContactPhone
        FROM [dbo].[EmergencyContact] ec
        WHERE ec.ReservationID = b.ReservationID
        FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
    )) AS EmergencyContact

FROM [dbo].[Booking] b
CROSS APPLY (
    SELECT TOP 1
        h.HotelID,
        h.Name,
        h.Address,
        h.Country,
        h.State,
        h.City,
        h.Phone,
        h.Email,
        h.IsActive
    FROM [dbo].[BookingRoom] br2
    JOIN [dbo].[Room] r ON br2.RoomID = r.RoomID
    JOIN [dbo].[Hotel] h ON r.HotelID = h.HotelID
    WHERE br2.ReservationID = b.ReservationID
) h
WHERE b.ReservationID = @RESERVATION_ID
ORDER BY b.ReservationID
		FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
		);
END
GO
/****** Object:  StoredProcedure [dbo].[BOOKING_INSERT]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:      KEVIN SUAREZ
-- Create date: 22/02/2025
-- Description: Creacion de una nueva reserva.
-- =============================================
CREATE PROCEDURE [dbo].[BOOKING_INSERT]
    @IN_DATA NVARCHAR(MAX),
    @OUT_ID INT OUTPUT,
    @OUT_ERROR_MESSAGE NVARCHAR(4000) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Extraer los datos del JSON
        DECLARE @UserID INT,
                @CheckIn DATETIME,
                @CheckOut DATETIME,
                @NumberGuests INT;

        SELECT 
            @UserID    = JSON_VALUE(@IN_DATA, '$.UserID'),
            @CheckIn     = JSON_VALUE(@IN_DATA, '$.CheckIn'),
            @CheckOut       = JSON_VALUE(@IN_DATA, '$.CheckOut'),
            @NumberGuests   = JSON_VALUE(@IN_DATA, '$.NumberGuests');

        -- Insertar los datos en la tabla [Room]
        INSERT INTO [dbo].[Booking] (
            [UserID],
            [CheckInDate],
            [CheckOutDate],
            [NumberOfGuests]
        )
        VALUES (
            @UserID,
            @CheckIn,
            @CheckOut,
            @NumberGuests
        );

        SET @OUT_ID = SCOPE_IDENTITY();
        SET @OUT_ERROR_MESSAGE = '';
    END TRY
    BEGIN CATCH
        SET @OUT_ID = -1;
        SET @OUT_ERROR_MESSAGE = ERROR_MESSAGE();
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[BOOKINGROOMS_INSERT]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:      KEVIN SUAREZ
-- Create date: 22/02/2025
-- Description: Creacion reserva de habitacion.
-- =============================================
CREATE PROCEDURE [dbo].[BOOKINGROOMS_INSERT]
    @ROOM_ID INT,
    @BOOKING_ID INT,
    @OUT_ERROR_MESSAGE NVARCHAR(4000) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY

        INSERT INTO [dbo].[BookingRoom] (
            [ReservationID],
            [RoomID]
        )
        VALUES (
            @BOOKING_ID,
            @ROOM_ID
        );

        SET @OUT_ERROR_MESSAGE = '';
    END TRY
    BEGIN CATCH
        SET @OUT_ERROR_MESSAGE = ERROR_MESSAGE();
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[EMERGENCYCONTACT_INSERT]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:      KEVIN SUAREZ
-- Create date: 22/02/2025
-- Description: Creacion de un nuevo huesped.
-- =============================================
CREATE PROCEDURE [dbo].[EMERGENCYCONTACT_INSERT]
    @IN_DATA NVARCHAR(MAX),
    @BOOKING_ID INT,
    @OUT_ERROR_MESSAGE NVARCHAR(4000) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Extraer los datos del JSON
        DECLARE @FirstName VARCHAR(100),
                @LastName VARCHAR(100),
				@ContactPhone VARCHAR(20);

        SELECT 
            @FirstName    = JSON_VALUE(@IN_DATA, '$.FirstName'),
            @LastName     = JSON_VALUE(@IN_DATA, '$.LastName'),
            @ContactPhone   = JSON_VALUE(@IN_DATA, '$.ContactPhone');

        -- Insertar los datos en la tabla [Room]
        INSERT INTO [dbo].[EmergencyContact] (
            [ReservationID],
            [FirstName],
            [LastName],
            [ContactPhone]
        )
        VALUES (
            @BOOKING_ID,
            @FirstName,
            @LastName,
            @ContactPhone
        );
        SET @OUT_ERROR_MESSAGE = '';
    END TRY
    BEGIN CATCH
        SET @OUT_ERROR_MESSAGE = ERROR_MESSAGE();
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[GUEST_GET_ALL]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author, KEVIN SUAREZ>
-- Create date: <Create Date, 22/02/2025>
-- Description:	<Description, Listar todos los huespedes>
-- =============================================
CREATE PROCEDURE [dbo].[GUEST_GET_ALL]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM dbo.Guest
END
GO
/****** Object:  StoredProcedure [dbo].[GUEST_INSERT]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:      KEVIN SUAREZ
-- Create date: 22/02/2025
-- Description: Creacion de un nuevo huesped.
-- =============================================
CREATE PROCEDURE [dbo].[GUEST_INSERT]
    @IN_DATA NVARCHAR(MAX),
    @BOOKING_ID INT,
    @OUT_ERROR_MESSAGE NVARCHAR(4000) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Extraer los datos del JSON
        DECLARE @FirstName VARCHAR(100),
                @LastName VARCHAR(100),
                @BirthDate DATETIME,
                @Gender VARCHAR(10),
				@DocumentType VARCHAR(20),
				@DocumentNumber VARCHAR(50),
				@Email VARCHAR(100),
				@ContactPhone VARCHAR(20);

        SELECT 
            @FirstName    = JSON_VALUE(@IN_DATA, '$.FirstName'),
            @LastName     = JSON_VALUE(@IN_DATA, '$.LastName'),
            @BirthDate       = JSON_VALUE(@IN_DATA, '$.BirthDate'),
            @Gender   = JSON_VALUE(@IN_DATA, '$.Gender'),
            @DocumentType   = JSON_VALUE(@IN_DATA, '$.DocumentType'),
            @DocumentNumber   = JSON_VALUE(@IN_DATA, '$.DocumentNumber'),
            @Email   = JSON_VALUE(@IN_DATA, '$.Email'),
            @ContactPhone   = JSON_VALUE(@IN_DATA, '$.ContactPhone');

        -- Insertar los datos en la tabla [Room]
        INSERT INTO [dbo].[Guest] (
            [ReservationID],
            [FirstName],
            [LastName],
            [BirthDate],
            [Gender],
            [DocumentType],
            [DocumentNumber],
            [Email],
            [ContactPhone]
        )
        VALUES (
            @BOOKING_ID,
            @FirstName,
            @LastName,
            @BirthDate,
            @Gender,
            @DocumentType,
            @DocumentNumber,
            @Email,
            @ContactPhone
        );
        SET @OUT_ERROR_MESSAGE = '';
    END TRY
    BEGIN CATCH
        SET @OUT_ERROR_MESSAGE = ERROR_MESSAGE();
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[HOTEL_GET_ALL]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author, KEVIN SUAREZ>
-- Create date: <Create Date, 22/02/2025>
-- Description:	<Description, Listar todos los hoteles>
-- =============================================
CREATE PROCEDURE [dbo].[HOTEL_GET_ALL]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [HotelID]
		  ,[Name]
		  ,[Address]
		  ,[City]
		  ,[State]
		  ,[Country]
		  ,[Phone]
		  ,[Email]
		  ,[IsActive]
	  FROM [dbo].[Hotel]
END
GO
/****** Object:  StoredProcedure [dbo].[HOTEL_GETBY_ID]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author, KEVIN SUAREZ>
-- Create date: <Create Date, 22/02/2025>
-- Description:	<Description, Buscar hotel por Id.>
-- =============================================
CREATE PROCEDURE [dbo].[HOTEL_GETBY_ID]
	@ID_HOTEL INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [HotelID]
		,[Name]
		,[Address]
		,[City]
		,[State]
		,[Country]
		,[Phone]
		,[Email]
		,[IsActive]
	FROM [dbo].[Hotel]
	WHERE [HotelID] = @ID_HOTEL;
END
GO
/****** Object:  StoredProcedure [dbo].[HOTEL_INSERT]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:      KEVIN SUAREZ
-- Create date: 22/02/2025
-- Description: Crear un nuevo Hotel y regresar el nuevo id creado.
-- =============================================
CREATE PROCEDURE [dbo].[HOTEL_INSERT]
    @IN_DATA NVARCHAR(MAX),
    @OUT_ID INT OUTPUT,
    @OUT_ERROR_MESSAGE NVARCHAR(4000) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Extraer los datos del JSON
        DECLARE @Name VARCHAR(100),
                @Address VARCHAR(250),
                @City VARCHAR(50),
                @State VARCHAR(50),
                @Country VARCHAR(50),
                @Phone VARCHAR(20),
                @Email VARCHAR(100);

        SELECT 
            @Name    = JSON_VALUE(@IN_DATA, '$.Name'),
            @Address = JSON_VALUE(@IN_DATA, '$.Address'),
            @City    = JSON_VALUE(@IN_DATA, '$.City'),
            @State   = JSON_VALUE(@IN_DATA, '$.State'),
            @Country = JSON_VALUE(@IN_DATA, '$.Country'),
            @Phone   = JSON_VALUE(@IN_DATA, '$.Phone'),
            @Email   = JSON_VALUE(@IN_DATA, '$.Email');

        -- Insertar los datos en la tabla [Hotel]
        INSERT INTO [dbo].[Hotel] (
            Name,
            Address,
            City,
            State,
            Country,
            Phone,
            Email,
            IsActive       -- Estado del hotel, 1 para activo
        )
        VALUES (
            @Name,
            @Address,
            @City,
            @State,
            @Country,
            @Phone,
            @Email,
            1          -- Hotel activo
        );

        SET @OUT_ID = SCOPE_IDENTITY();

		SET @OUT_ERROR_MESSAGE = '';	
    END TRY
    BEGIN CATCH
        SET @OUT_ID = -1;
        SET @OUT_ERROR_MESSAGE = ERROR_MESSAGE();
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[HOTEL_UPDATE]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:      KEVIN SUAREZ
-- Create date: 22/02/2025
-- Description: Actualizar un hotel.
-- =============================================
CREATE PROCEDURE [dbo].[HOTEL_UPDATE]
    @IN_DATA NVARCHAR(MAX),
    @OUT_CONFIRM BIT OUTPUT,
    @OUT_ERROR_MESSAGE NVARCHAR(4000) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Extraer los datos del JSON
        DECLARE @HotelId INT,
				@Name VARCHAR(100),
                @Address VARCHAR(250),
                @City VARCHAR(50),
                @State VARCHAR(50),
                @Country VARCHAR(50),
                @Phone VARCHAR(20),
                @Email VARCHAR(100);

        SELECT 
            @HotelId    = JSON_VALUE(@IN_DATA, '$.Id'),
            @Name    = JSON_VALUE(@IN_DATA, '$.Name'),
            @City    = JSON_VALUE(@IN_DATA, '$.City'),
            @Address = JSON_VALUE(@IN_DATA, '$.Address'),
            @State   = JSON_VALUE(@IN_DATA, '$.State'),
            @Country = JSON_VALUE(@IN_DATA, '$.Country'),
            @Phone   = JSON_VALUE(@IN_DATA, '$.Phone'),
            @Email   = JSON_VALUE(@IN_DATA, '$.Email');

		UPDATE [dbo].[Hotel]
		   SET [Name] = @Name
			  ,[Address] = @Address
			  ,[City] = @City
			  ,[State] = @State
			  ,[Country] = @Country
			  ,[Phone] = @Phone
			  ,[Email] = @Email
		 WHERE [HotelID] = @HotelId

        SET @OUT_CONFIRM = 1;

		SET @OUT_ERROR_MESSAGE = '';	
    END TRY
    BEGIN CATCH
        SET @OUT_CONFIRM = 0;
        SET @OUT_ERROR_MESSAGE = ERROR_MESSAGE();
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[HOTEL_UPDATE_ISACTIVE]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:      KEVIN SUAREZ
-- Create date: 22/02/2025
-- Description: Actualizar estado de un hotel.
-- =============================================
CREATE PROCEDURE [dbo].[HOTEL_UPDATE_ISACTIVE]
    @HOTEL_ID INT,
    @IS_ACTIVE INT,
    @OUT_CONFIRM BIT OUTPUT,
    @OUT_ERROR_MESSAGE NVARCHAR(4000) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY

		UPDATE [dbo].[Hotel]
		   SET [IsActive] = @IS_ACTIVE
		 WHERE [HotelID] = @HOTEL_ID

        SET @OUT_CONFIRM = 1;

		SET @OUT_ERROR_MESSAGE = '';	
    END TRY
    BEGIN CATCH
        SET @OUT_CONFIRM = 0;
        SET @OUT_ERROR_MESSAGE = ERROR_MESSAGE();
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[ROLE_GET_ALL]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author, KEVIN SUAREZ>
-- Create date: <Create Date, 22/02/2025>
-- Description:	<Description, Listar todas los tipos de roles de usuarios>
-- =============================================
CREATE PROCEDURE [dbo].[ROLE_GET_ALL]
AS
BEGIN
	SET NOCOUNT ON;
		
	SELECT [RoleID]
		  ,[RoleName]
		  ,[Description]
		  ,[IsActive]
	  FROM [dbo].[Role]
END
GO
/****** Object:  StoredProcedure [dbo].[ROOM_FINDBY_FILTERS]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author, KEVIN SUAREZ>
-- Create date: <Create Date, 22/02/2025>
-- Description:	<Description, Obtener habitaciones disponibles segun filtros de busqueda>
-- =============================================
CREATE PROCEDURE [dbo].[ROOM_FINDBY_FILTERS]
    @OUT_DATA NVARCHAR(MAX) OUTPUT,
    @CITY VARCHAR(50),
    @CHECK_IN DATETIME,
    @CHECK_OUT DATETIME
AS
BEGIN
	SET NOCOUNT ON;

	SELECT @OUT_DATA = ((SELECT 
		h.HotelID,
		h.Name,
		h.Address,
		h.Country,
		h.State,
		h.City,
		h.Phone,
		h.Email,
		r.RoomID,
		r.Number,
		JSON_QUERY((
			SELECT 
				rt.TypeID,
				rt.NameType,
				rt.NumberOfGuests
			FROM [dbo].[RoomType] rt
			WHERE rt.TypeID = r.TypeID
			FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
		)) AS TypeRoom,
		r.BaseCost,
		r.Taxes,
		r.Location
	FROM [dbo].[Hotel] h
	JOIN [dbo].[Room] r ON h.HotelID = r.HotelID
	JOIN [dbo].[RoomType] rt ON r.TypeID = rt.TypeID
	WHERE 
		h.City = @CITY
		AND h.IsActive = 1
		AND r.IsActive = 1
		-- No se filtra por capacidad, ya que la reserva podría combinar varias habitaciones
		AND NOT EXISTS (
			 SELECT 1
			 FROM [dbo].[BookingRoom] br
			 JOIN [dbo].[Booking] b ON br.ReservationID = b.ReservationID
			 WHERE br.RoomID = r.RoomID
			   AND b.ReservationStatus = 1
			   AND br.IsActive = 1
			   -- Se valida que no haya reservas activas que solapen con las fechas solicitadas
			   AND b.CheckInDate < @CHECK_OUT
			   AND b.CheckOutDate > @CHECK_IN
		)
	FOR JSON PATH));
END
GO
/****** Object:  StoredProcedure [dbo].[ROOM_GET_ALL]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author, KEVIN SUAREZ>
-- Create date: <Create Date, 22/02/2025>
-- Description:	<Description, Listar todas las habitaciones>
-- =============================================
CREATE PROCEDURE [dbo].[ROOM_GET_ALL]
    @OUT_DATA NVARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT @OUT_DATA = (SELECT [RoomID]
		  ,[HotelID]
		  ,[Number]
		  ,JSON_QUERY((
				SELECT 
					rt.TypeID,
					rt.NameType,
					rt.NumberOfGuests
				FROM [dbo].[RoomType] rt
				WHERE rt.TypeID = r.TypeID
				FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
			)) AS TypeRoom
		  ,[BaseCost]
		  ,[Taxes]
		  ,[Location]
		  ,[IsActive]
	  FROM [dbo].[Room] r
	  FOR JSON PATH);
END
GO
/****** Object:  StoredProcedure [dbo].[ROOM_GET_ALL_TYPES]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author, KEVIN SUAREZ>
-- Create date: <Create Date, 22/02/2025>
-- Description:	<Description, Listar todos los tipos de habitaciones>
-- =============================================
CREATE PROCEDURE [dbo].[ROOM_GET_ALL_TYPES]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [TypeID]
		  ,[NameType]
		  ,[NumberOfGuests]
	  FROM [dbo].[RoomType]
END
GO
/****** Object:  StoredProcedure [dbo].[ROOM_GETBY_ID]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author, KEVIN SUAREZ>
-- Create date: <Create Date, 22/02/2025>
-- Description:	<Description, Obtener la informacion de una habitacion>
-- =============================================
CREATE PROCEDURE [dbo].[ROOM_GETBY_ID]
	@ROOM_ID INT,
	@HOTEL_ID INT,
    @OUT_DATA NVARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT @OUT_DATA = (SELECT r.[RoomID]
		  ,r.[HotelID]
		  ,r.[Number]
		  ,JSON_QUERY((
				SELECT 
					rt.TypeID,
					rt.NameType,
					rt.NumberOfGuests
				FROM [dbo].[RoomType] rt
				WHERE rt.TypeID = r.TypeID
				FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
			)) AS TypeRoom
		  ,r.[BaseCost]
		  ,r.[Taxes]
		  ,r.[Location]
		  ,r.[IsActive]
	  FROM [dbo].[Room] r
	  WHERE
		r.[RoomID] = @ROOM_ID AND r.[HotelID] = @HOTEL_ID FOR JSON PATH, WITHOUT_ARRAY_WRAPPER);
END
GO
/****** Object:  StoredProcedure [dbo].[ROOM_INSERT]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:      KEVIN SUAREZ
-- Create date: 22/02/2025
-- Description: Crear una nueva habitación y regresar el nuevo id creado. 
--              Se agrega un parámetro de salida adicional para el mensaje de error.
-- =============================================
CREATE PROCEDURE [dbo].[ROOM_INSERT]
    @IN_DATA NVARCHAR(MAX),
    @OUT_ID INT OUTPUT,
    @OUT_ERROR_MESSAGE NVARCHAR(4000) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Extraer los datos del JSON
        DECLARE @HotelId INT,
                @Number VARCHAR(10),
                @TypeID VARCHAR(100),
                @BaseCost DECIMAL(10, 2),
                @Taxes DECIMAL(5, 2),
                @Location VARCHAR(100);

        SELECT 
            @HotelId    = JSON_VALUE(@IN_DATA, '$.HotelId'),
            @Number     = JSON_VALUE(@IN_DATA, '$.Number'),
            @TypeID       = JSON_VALUE(@IN_DATA, '$.Type'),
            @BaseCost   = JSON_VALUE(@IN_DATA, '$.BaseCost'),
            @Taxes      = JSON_VALUE(@IN_DATA, '$.Taxes'),
            @Location   = JSON_VALUE(@IN_DATA, '$.Location');

        -- Insertar los datos en la tabla [Room]
        INSERT INTO [dbo].[Room] (
            [HotelID],
            [Number],
            [TypeID],
            [BaseCost],
            [Taxes],
            [Location],
            [IsActive]       -- Estado de la habitación, 1 para activo
        )
        VALUES (
            @HotelId,
            @Number,
            @TypeID,
            @BaseCost,
            @Taxes,
            @Location,
            1          -- Habitación activa
        );

        SET @OUT_ID = SCOPE_IDENTITY();
        SET @OUT_ERROR_MESSAGE = '';
    END TRY
    BEGIN CATCH
        SET @OUT_ID = -1;
        SET @OUT_ERROR_MESSAGE = ERROR_MESSAGE();
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[ROOM_UPDATE]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:      KEVIN SUAREZ
-- Create date: 22/02/2025
-- Description: Actualizar una habitacion.
-- =============================================
CREATE PROCEDURE [dbo].[ROOM_UPDATE]
    @IN_DATA NVARCHAR(MAX),
    @OUT_CONFIRM BIT OUTPUT,
    @OUT_ERROR_MESSAGE NVARCHAR(4000) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Extraer los datos del JSON
        DECLARE @RoomId INT,
				@HotelId INT,
                @Number VARCHAR(10),
                @TypeID INT,
                @BaseCost DECIMAL(10, 2),
                @Taxes DECIMAL(5, 2),
                @Location VARCHAR(100);

        SELECT 
            @RoomId    = JSON_VALUE(@IN_DATA, '$.Id'),
            @HotelId    = JSON_VALUE(@IN_DATA, '$.HotelId'),
            @Number     = JSON_VALUE(@IN_DATA, '$.Number'),
            @TypeID       = JSON_VALUE(@IN_DATA, '$.Type'),
            @BaseCost   = JSON_VALUE(@IN_DATA, '$.BaseCost'),
            @Taxes      = JSON_VALUE(@IN_DATA, '$.Taxes'),
            @Location   = JSON_VALUE(@IN_DATA, '$.Location');

		UPDATE [dbo].[Room]
		   SET [HotelID] = @HotelId
			  ,[Number] = @Number
			  ,[TypeID] = @TypeID
			  ,[BaseCost] = @BaseCost
			  ,[Taxes] = @Taxes
			  ,[Location] = @Location
		 WHERE [HotelID] = @HotelId AND [RoomId] = @RoomId

        SET @OUT_CONFIRM = 1;

		SET @OUT_ERROR_MESSAGE = '';	
    END TRY
    BEGIN CATCH
        SET @OUT_CONFIRM = 0;
        SET @OUT_ERROR_MESSAGE = ERROR_MESSAGE();
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[ROOM_UPDATE_ISACTIVE]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:      KEVIN SUAREZ
-- Create date: 22/02/2025
-- Description: Actualizar esatdo de una habitacion.
-- =============================================
CREATE PROCEDURE [dbo].[ROOM_UPDATE_ISACTIVE]
    @ROOM_ID INT,
    @IS_ACTIVE INT,
    @OUT_CONFIRM BIT OUTPUT,
    @OUT_ERROR_MESSAGE NVARCHAR(4000) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY

		UPDATE [dbo].[Room]
		   SET [IsActive] = @IS_ACTIVE
		 WHERE [RoomId] = @ROOM_ID

        SET @OUT_CONFIRM = 1;

		SET @OUT_ERROR_MESSAGE = '';	
    END TRY
    BEGIN CATCH
        SET @OUT_CONFIRM = 0;
        SET @OUT_ERROR_MESSAGE = ERROR_MESSAGE();
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[USER_INSERT]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author, KEVIN SUAREZ>
-- Create date: <Create Date, 22/02/2025>
-- Description:	<Description, Crear un nuevo usuario.>
-- =============================================
CREATE PROCEDURE [dbo].[USER_INSERT]
    @IN_DATA NVARCHAR(MAX),
    @OUT_ID INT OUTPUT,
    @OUT_ERROR_MESSAGE NVARCHAR(4000) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Extraer los datos del JSON
        DECLARE @Username NVARCHAR(50),
                @PasswordHash NVARCHAR(255),
                @Email NVARCHAR(100),
                @FirstName NVARCHAR(100),
                @LastName NVARCHAR(100);

        SELECT @Username = JSON_VALUE(@IN_DATA, '$.UserName'),
               @PasswordHash = JSON_VALUE(@IN_DATA, '$.Password'),
               @Email = JSON_VALUE(@IN_DATA, '$.Email'),
               @FirstName = JSON_VALUE(@IN_DATA, '$.FirstName'),
               @LastName = JSON_VALUE(@IN_DATA, '$.LastName');

        -- Insertar los datos en la tabla [User]
        INSERT INTO [dbo].[User] (
            Username,
            PasswordHash,
            Email,
            FirstName,
            LastName,
            RegistrationDate,
            IsActive
        )
        VALUES (
            @Username,
            @PasswordHash,
            @Email,
            @FirstName,
            @LastName,
            GETDATE(), -- Fecha de registro actual
            1          -- Usuario activo
        );

        -- Indicar éxito
        SET @OUT_ID = SCOPE_IDENTITY();
        SET @OUT_ERROR_MESSAGE = '';
    END TRY
    BEGIN CATCH
        SET @OUT_ID = -1;
        SET @OUT_ERROR_MESSAGE = ERROR_MESSAGE();
    END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[USER_LIST]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author, KEVIN SUAREZ>
-- Create date: <Create Date, 22/02/2025>
-- Description:	<Description, Obtener todos los usuarios.>
-- =============================================
CREATE PROCEDURE [dbo].[USER_LIST]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM [dbo].[User]
END
GO
/****** Object:  StoredProcedure [dbo].[USER_ROLE_INSERT]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:      KEVIN SUAREZ
-- Create date: 22/02/2025
-- Description: Se agrega un nuevo rol a un usuario.
-- =============================================
CREATE PROCEDURE [dbo].[USER_ROLE_INSERT]
    @USER_ID INT,
    @ROLE_ID INT,
    @OUT_DATA BIT OUTPUT,
    @OUT_ERROR_MESSAGE NVARCHAR(4000) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY

        INSERT INTO [dbo].[UserRole]
			   ([UserID]
			   ,[RoleID]
			   ,[IsActive])
		VALUES
			   (@USER_ID
			   ,@ROLE_ID
			   ,1);

        SET @OUT_DATA = 1;
        SET @OUT_ERROR_MESSAGE = '';
    END TRY
    BEGIN CATCH
        SET @OUT_DATA = 0;
        SET @OUT_ERROR_MESSAGE = ERROR_MESSAGE();
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[USER_ROLE_SEARCHBY_ID]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author, KEVIN SUAREZ>
-- Create date: <Create Date, 22/02/2025>
-- Description:	<Description, Obtener todos los roles asignados a un usuario>
-- =============================================
CREATE PROCEDURE [dbo].[USER_ROLE_SEARCHBY_ID]
	@USER_ID INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		sr.UserID,
		sr.RoleID,
		r.RoleName,
		sr.IsActive
	FROM 
		[dbo].[UserRole] as sr
	JOIN [dbo].[Role] as r ON sr.RoleID = r.RoleID
	WHERE UserID = @USER_ID;
END
GO
/****** Object:  StoredProcedure [dbo].[USER_ROLE_UPTADE]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:      KEVIN SUAREZ
-- Create date: 22/02/2025
-- Description: Actualiza rol de usuario.
-- =============================================
CREATE PROCEDURE [dbo].[USER_ROLE_UPTADE]
    @USER_ID INT,
    @ROLE_ID INT,
    @STATE BIT,
    @OUT_DATA BIT OUTPUT,
    @OUT_ERROR_MESSAGE NVARCHAR(4000) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY

        UPDATE [dbo].[UserRole]
		SET
			[IsActive] = @STATE
		WHERE 
			[UserID] = @USER_ID AND [RoleID] = @ROLE_ID;

        SET @OUT_DATA = 1;
        SET @OUT_ERROR_MESSAGE = '';
    END TRY
    BEGIN CATCH
        SET @OUT_DATA = 0;
        SET @OUT_ERROR_MESSAGE = ERROR_MESSAGE();
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[USER_SEARCHBY_USERNAME]    Script Date: 24/02/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author, KEVIN SUAREZ>
-- Create date: <Create Date, 22/02/2025>
-- Description:	<Description, Obtener informacion de usuario por el UserName>
-- =============================================
CREATE PROCEDURE [dbo].[USER_SEARCHBY_USERNAME]
	@USER_NAME VARCHAR(250)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM [dbo].[User] WHERE Username = @USER_NAME;
END
GO

CREATE TRIGGER TR_BookingRoom_NoOverlap
ON [dbo].[BookingRoom]
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Se compara cada registro insertado/actualizado (alias i)
    -- con otros registros activos existentes para la misma habitación
    -- que pertenezcan a reservas con fechas (CheckInDate y CheckOutDate)
    -- y se verifica que no se solapen.
    IF EXISTS (
        SELECT 1
        FROM inserted i
        INNER JOIN [dbo].[Booking] bNew 
            ON i.ReservationID = bNew.ReservationID
        INNER JOIN [dbo].[BookingRoom] brExisting
            ON brExisting.RoomID = i.RoomID
               AND brExisting.IsActive = 1
               -- Excluir el mismo registro en caso de update:
               AND NOT (brExisting.ReservationID = i.ReservationID AND brExisting.RoomID = i.RoomID)
        INNER JOIN [dbo].[Booking] bExisting 
            ON brExisting.ReservationID = bExisting.ReservationID
        WHERE i.IsActive = 1
          -- Verifica que los periodos se solapen:
          AND bNew.CheckInDate < bExisting.CheckOutDate
          AND bNew.CheckOutDate > bExisting.CheckInDate
    )
    BEGIN
         RAISERROR('La habitación ya tiene una reserva activa en un periodo que se solapa.', 16, 1);
         RETURN;
    END
END;
GO