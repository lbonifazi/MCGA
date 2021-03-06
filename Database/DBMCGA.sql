USE [master]
GO
/****** Object:  Database [SalesBooks]    Script Date: 12/22/2016 10:48:50 AM ******/
CREATE DATABASE [SalesBooks]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SalesBooks', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\SalesBooks.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SalesBooks_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\SalesBooks_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [SalesBooks] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SalesBooks].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SalesBooks] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SalesBooks] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SalesBooks] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SalesBooks] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SalesBooks] SET ARITHABORT OFF 
GO
ALTER DATABASE [SalesBooks] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SalesBooks] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SalesBooks] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SalesBooks] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SalesBooks] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SalesBooks] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SalesBooks] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SalesBooks] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SalesBooks] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SalesBooks] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SalesBooks] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SalesBooks] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SalesBooks] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SalesBooks] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SalesBooks] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SalesBooks] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SalesBooks] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SalesBooks] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SalesBooks] SET  MULTI_USER 
GO
ALTER DATABASE [SalesBooks] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SalesBooks] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SalesBooks] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SalesBooks] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SalesBooks] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SalesBooks] SET QUERY_STORE = OFF
GO
USE [SalesBooks]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [SalesBooks]
GO
/****** Object:  Table [dbo].[Author]    Script Date: 12/22/2016 10:48:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Author](
	[AuthorID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
 CONSTRAINT [PK_Author] PRIMARY KEY CLUSTERED 
(
	[AuthorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Book]    Script Date: 12/22/2016 10:48:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book](
	[BookId] [int] IDENTITY(1,1) NOT NULL,
	[ISBN] [varchar](50) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[Edition] [varchar](5) NULL,
	[Volume] [varchar](5) NULL,
	[Abstract] [varchar](max) NULL,
	[PublisherId] [int] NOT NULL,
	[PublishYear] [int] NULL,
 CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED 
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Book_Author]    Script Date: 12/22/2016 10:48:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book_Author](
	[BookId] [int] NOT NULL,
	[AuthorId] [int] NOT NULL,
 CONSTRAINT [PK_Book_Author] PRIMARY KEY CLUSTERED 
(
	[BookId] ASC,
	[AuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Book_Review]    Script Date: 12/22/2016 10:48:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book_Review](
	[ReviewId] [int] NOT NULL,
	[BookId] [int] NOT NULL,
 CONSTRAINT [PK_Book_Review] PRIMARY KEY CLUSTERED 
(
	[ReviewId] ASC,
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Book_Subject]    Script Date: 12/22/2016 10:48:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book_Subject](
	[BookId] [int] NOT NULL,
	[SubjectId] [int] NOT NULL,
 CONSTRAINT [PK_Book_Subject] PRIMARY KEY CLUSTERED 
(
	[BookId] ASC,
	[SubjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Price]    Script Date: 12/22/2016 10:48:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Price](
	[BookId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Cost] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_Price] PRIMARY KEY CLUSTERED 
(
	[BookId] ASC,
	[Date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Publisher]    Script Date: 12/22/2016 10:48:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Publisher](
	[PublisherID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Publisher] PRIMARY KEY CLUSTERED 
(
	[PublisherID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Review]    Script Date: 12/22/2016 10:48:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Review](
	[ReviewId] [int] IDENTITY(1,1) NOT NULL,
	[Text] [varchar](max) NULL,
	[UserId] [int] NOT NULL,
	[Star] [int] NOT NULL,
 CONSTRAINT [PK_Review] PRIMARY KEY CLUSTERED 
(
	[ReviewId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Subject]    Script Date: 12/22/2016 10:48:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject](
	[SubjectId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
 CONSTRAINT [PK_Subject] PRIMARY KEY CLUSTERED 
(
	[SubjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 12/22/2016 10:48:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[UserType] [int] NOT NULL,
	[CreateDt] [date] NOT NULL,
	[Disable] [bit] NOT NULL,
	[PasswordTempInd] [bit] NOT NULL,
	[ActivationCode] [varchar](200) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Author] ON 

INSERT [dbo].[Author] ([AuthorID], [FirstName], [LastName]) VALUES (1, N'Aaron', N'Ailis')
INSERT [dbo].[Author] ([AuthorID], [FirstName], [LastName]) VALUES (2, N'Alejandro', N'Dumas')
INSERT [dbo].[Author] ([AuthorID], [FirstName], [LastName]) VALUES (3, N'Jorge Luis', N'Borges')
INSERT [dbo].[Author] ([AuthorID], [FirstName], [LastName]) VALUES (4, N'Alan', N'Moore')
SET IDENTITY_INSERT [dbo].[Author] OFF
SET IDENTITY_INSERT [dbo].[Book] ON 

INSERT [dbo].[Book] ([BookId], [ISBN], [Title], [Edition], [Volume], [Abstract], [PublisherId], [PublishYear]) VALUES (6, N'155-34-22-24377-7', N'El Conde de Montecristo', N'1', N'11', N'La historia tiene lugar en Francia, Italia y varias islas del Mediterráneo durante los hechos históricos de 1814–1838 (Los Cien Días del gobierno de Napoleón I, el reinado de Luis XVIII de Francia, de Carlos X de Francia y el reinado de Luis Felipe I de Francia). Trata sobre todo los temas de la justicia, la venganza, la piedad y el perdón y está contada en el estilo de una historia de aventuras.', 1, 1991)
INSERT [dbo].[Book] ([BookId], [ISBN], [Title], [Edition], [Volume], [Abstract], [PublisherId], [PublishYear]) VALUES (7, N'122-84-01-01111-7', N'Alicia en el Pais de las Maravillas', N'6', N'2', N'Alicia en el país de las maravillas, es una obra de literatura creada por el matemático, sacerdote anglicano y escritor británico Charles Lutwidge Dodgson, bajo el más conocido seudónimo de Lewis Carroll. El cuento está lleno de alusiones satíricas a los amigos de Dodgson, la educación inglesa y temas políticos de la época. El país de las maravillas que se describe en la historia es fundamentalmente creado a través de juegos con la lógica, de una forma tan especial, que la obra ha llegado a tener popularidad en los más variados ambientes, desde niños o matemáticos hasta psiconautas.', 6, 1991)
INSERT [dbo].[Book] ([BookId], [ISBN], [Title], [Edition], [Volume], [Abstract], [PublisherId], [PublishYear]) VALUES (9, N'544-55-02-05223-7', N'Draculas', NULL, NULL, N'Drácula (Vlad Draculea) es el protagonista de la novela homónima del irlandés Bram Stoker, de 1897, que dio lugar a una larga lista de versiones de cine, cómics y teatro. Drácula es el más famoso de los «vampiros humanos». Se dice que Stoker fue asesorado por un erudito en temas orientales, el húngaro Hermann (Arminius) Vámbéry, que se reunió algunas veces con el escritor para comentarle las peripecias del verdadero Drácula.', 2, 1985)
INSERT [dbo].[Book] ([BookId], [ISBN], [Title], [Edition], [Volume], [Abstract], [PublisherId], [PublishYear]) VALUES (11, N'978-84-02-0837', N'El Principito', N'1', NULL, N'El principito (título original en francés: Le Petit Prince) es la más famosa novela escrita por el aviador y escritor Antoine de Saint-Exupéry. Fue publicada por primera vez el 6 de abril de 1943, cuando vivía exiliado en Estados Unidos tras la caída de Francia durante la Segunda Guerra Mundial. Es un cuento infantil que desde su apariencia sencilla ha llegado a considerarse una obra universal, siendo traducida a 160 lenguas y dialectos, llegando a convertirse en uno de los mayores éxitos de ventas de todos los tiempos, es el libro francés más vendido del mundo.', 3, 1996)
INSERT [dbo].[Book] ([BookId], [ISBN], [Title], [Edition], [Volume], [Abstract], [PublisherId], [PublishYear]) VALUES (14, N'123', N'aaaa', N'1', N'1', N'asd', 6, 1900)
INSERT [dbo].[Book] ([BookId], [ISBN], [Title], [Edition], [Volume], [Abstract], [PublisherId], [PublishYear]) VALUES (15, N'13123', N'bbbb', N'1', N'1', N'resumen', 4, 1998)
INSERT [dbo].[Book] ([BookId], [ISBN], [Title], [Edition], [Volume], [Abstract], [PublisherId], [PublishYear]) VALUES (16, N'13123', N'bbbb', N'1', N'1', N'resumen', 4, 1998)
INSERT [dbo].[Book] ([BookId], [ISBN], [Title], [Edition], [Volume], [Abstract], [PublisherId], [PublishYear]) VALUES (17, N'123', N'ccc', N'1', N'1', N'1', 6, 11)
SET IDENTITY_INSERT [dbo].[Book] OFF
INSERT [dbo].[Book_Author] ([BookId], [AuthorId]) VALUES (6, 2)
INSERT [dbo].[Book_Author] ([BookId], [AuthorId]) VALUES (7, 1)
INSERT [dbo].[Book_Author] ([BookId], [AuthorId]) VALUES (7, 3)
INSERT [dbo].[Book_Author] ([BookId], [AuthorId]) VALUES (9, 1)
INSERT [dbo].[Book_Author] ([BookId], [AuthorId]) VALUES (11, 2)
INSERT [dbo].[Book_Author] ([BookId], [AuthorId]) VALUES (11, 3)
INSERT [dbo].[Book_Author] ([BookId], [AuthorId]) VALUES (14, 1)
INSERT [dbo].[Book_Review] ([ReviewId], [BookId]) VALUES (1, 6)
INSERT [dbo].[Book_Review] ([ReviewId], [BookId]) VALUES (9, 7)
INSERT [dbo].[Book_Review] ([ReviewId], [BookId]) VALUES (10, 7)
INSERT [dbo].[Book_Review] ([ReviewId], [BookId]) VALUES (11, 9)
INSERT [dbo].[Book_Review] ([ReviewId], [BookId]) VALUES (12, 11)
INSERT [dbo].[Book_Review] ([ReviewId], [BookId]) VALUES (13, 11)
INSERT [dbo].[Book_Review] ([ReviewId], [BookId]) VALUES (14, 11)
INSERT [dbo].[Book_Subject] ([BookId], [SubjectId]) VALUES (6, 6)
INSERT [dbo].[Book_Subject] ([BookId], [SubjectId]) VALUES (7, 7)
INSERT [dbo].[Book_Subject] ([BookId], [SubjectId]) VALUES (9, 6)
INSERT [dbo].[Book_Subject] ([BookId], [SubjectId]) VALUES (11, 1)
INSERT [dbo].[Book_Subject] ([BookId], [SubjectId]) VALUES (14, 5)
INSERT [dbo].[Price] ([BookId], [Date], [Cost]) VALUES (6, CAST(N'2016-01-01T00:00:00.000' AS DateTime), CAST(2750.00 AS Decimal(10, 2)))
INSERT [dbo].[Price] ([BookId], [Date], [Cost]) VALUES (7, CAST(N'2015-01-01T00:00:00.000' AS DateTime), CAST(3125.00 AS Decimal(10, 2)))
INSERT [dbo].[Price] ([BookId], [Date], [Cost]) VALUES (9, CAST(N'2016-01-01T00:00:00.000' AS DateTime), CAST(750.00 AS Decimal(10, 2)))
INSERT [dbo].[Price] ([BookId], [Date], [Cost]) VALUES (9, CAST(N'2016-07-01T00:00:00.000' AS DateTime), CAST(850.00 AS Decimal(10, 2)))
INSERT [dbo].[Price] ([BookId], [Date], [Cost]) VALUES (11, CAST(N'2016-02-01T00:00:00.000' AS DateTime), CAST(1130.00 AS Decimal(10, 2)))
INSERT [dbo].[Price] ([BookId], [Date], [Cost]) VALUES (14, CAST(N'2016-03-01T00:00:00.000' AS DateTime), CAST(100.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[Publisher] ON 

INSERT [dbo].[Publisher] ([PublisherID], [Name]) VALUES (1, N'HarperResource')
INSERT [dbo].[Publisher] ([PublisherID], [Name]) VALUES (2, N'Editorial De Colores ')
INSERT [dbo].[Publisher] ([PublisherID], [Name]) VALUES (3, N'Editorial Edb ')
INSERT [dbo].[Publisher] ([PublisherID], [Name]) VALUES (4, N'Editorial Peniel')
INSERT [dbo].[Publisher] ([PublisherID], [Name]) VALUES (5, N'Libros del Zorzal ')
INSERT [dbo].[Publisher] ([PublisherID], [Name]) VALUES (6, N'Paradigma Libros')
INSERT [dbo].[Publisher] ([PublisherID], [Name]) VALUES (7, N'Sessa Editores')
INSERT [dbo].[Publisher] ([PublisherID], [Name]) VALUES (8, N'Prícolo S.A.')
SET IDENTITY_INSERT [dbo].[Publisher] OFF
SET IDENTITY_INSERT [dbo].[Review] ON 

INSERT [dbo].[Review] ([ReviewId], [Text], [UserId], [Star]) VALUES (1, NULL, 1, 3)
INSERT [dbo].[Review] ([ReviewId], [Text], [UserId], [Star]) VALUES (9, NULL, 10, 4)
INSERT [dbo].[Review] ([ReviewId], [Text], [UserId], [Star]) VALUES (10, NULL, 11, 5)
INSERT [dbo].[Review] ([ReviewId], [Text], [UserId], [Star]) VALUES (11, NULL, 11, 3)
INSERT [dbo].[Review] ([ReviewId], [Text], [UserId], [Star]) VALUES (12, NULL, 11, 2)
INSERT [dbo].[Review] ([ReviewId], [Text], [UserId], [Star]) VALUES (13, NULL, 12, 3)
INSERT [dbo].[Review] ([ReviewId], [Text], [UserId], [Star]) VALUES (14, NULL, 13, 4)
SET IDENTITY_INSERT [dbo].[Review] OFF
SET IDENTITY_INSERT [dbo].[Subject] ON 

INSERT [dbo].[Subject] ([SubjectId], [Name]) VALUES (1, N'Medicina')
INSERT [dbo].[Subject] ([SubjectId], [Name]) VALUES (2, N'Ciencias Naturales')
INSERT [dbo].[Subject] ([SubjectId], [Name]) VALUES (3, N'Religion')
INSERT [dbo].[Subject] ([SubjectId], [Name]) VALUES (4, N'Ficcion')
INSERT [dbo].[Subject] ([SubjectId], [Name]) VALUES (5, N'Leyes')
INSERT [dbo].[Subject] ([SubjectId], [Name]) VALUES (6, N'Clásicos de la literatura')
INSERT [dbo].[Subject] ([SubjectId], [Name]) VALUES (7, N'Infantil y juvenil')
INSERT [dbo].[Subject] ([SubjectId], [Name]) VALUES (8, N'Aventura')
SET IDENTITY_INSERT [dbo].[Subject] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserId], [UserName], [Email], [Password], [UserType], [CreateDt], [Disable], [PasswordTempInd], [ActivationCode]) VALUES (1, N'Roberto Pizza', N'rpizza@gmail.com', N'EAAAAIMBbg1MboG9h7Z5ymHpFL7GWw5Xa54bBGA0JErwkaMu', 0, CAST(N'2016-10-18' AS Date), 0, 0, NULL)
INSERT [dbo].[User] ([UserId], [UserName], [Email], [Password], [UserType], [CreateDt], [Disable], [PasswordTempInd], [ActivationCode]) VALUES (10, N'Leonel Bonifazi', N'leonel.bonifazi@gmail.com', N'EAAAAOKb81XeD3NUez4E9tJrvXSiBgGBQPyuGv6lYgXrpGf5', 1, CAST(N'2016-10-21' AS Date), 0, 0, N'EAAAAIMkJQsungICrYqRHyjS1v7BnpyJbBES7PLFzx51ZzLIX5/6hS4S/QiykwczRVDn1A==')
INSERT [dbo].[User] ([UserId], [UserName], [Email], [Password], [UserType], [CreateDt], [Disable], [PasswordTempInd], [ActivationCode]) VALUES (11, N'Ramiro Zaza', N'rzaza@gmail.com', N'EAAAAEnJMCCj0iVxevRLED877J7b++1eOB9wzvPQq9BH/7EZ', 0, CAST(N'2016-11-04' AS Date), 1, 0, N'EAAAAMp8s0KhoWds1alQ9pi7quVyO/3jiVfVjKj8CcP8SX3h')
INSERT [dbo].[User] ([UserId], [UserName], [Email], [Password], [UserType], [CreateDt], [Disable], [PasswordTempInd], [ActivationCode]) VALUES (12, N'Eze Pochiero', N'epochiero@gmail.com', N'EAAAAHnFUObd6NQmRpG2ucd/uVkX87xKFhd/5lVF6qnKgHtp', 0, CAST(N'2016-11-04' AS Date), 0, 0, N'EAAAALXOwTN9/tnHEunBhiqedAMFdx8+d4IkxXauSiPNYp35')
INSERT [dbo].[User] ([UserId], [UserName], [Email], [Password], [UserType], [CreateDt], [Disable], [PasswordTempInd], [ActivationCode]) VALUES (13, N'Silvia Matero', N'smatero@gmail.com', N'EAAAAMxjMrdn4MBeoNs7kuATpdqHaaaNDt37Ja2CsQkDMo0v', 0, CAST(N'2016-11-04' AS Date), 1, 0, N'EAAAAKJ+pZxZODNOHF2F1wOQvN+GAPny6Hqv3FXaqP/09Es7WJVlpSSpOLX/U8d9N7E0Zg==')
INSERT [dbo].[User] ([UserId], [UserName], [Email], [Password], [UserType], [CreateDt], [Disable], [PasswordTempInd], [ActivationCode]) VALUES (14, N'LUCHO CONLEY', N'SNYDER@gmail.com', N'EAAAAIR2Mw5sHRtRIj+Hy2Rm8RvTWSVlb/tVvzljr6nBhvT2', 0, CAST(N'2016-11-04' AS Date), 1, 0, N'EAAAAMjmOlO85rTXrM/tAke7DC9bCbXvx1Edb6FjTv2aq8Ny')
INSERT [dbo].[User] ([UserId], [UserName], [Email], [Password], [UserType], [CreateDt], [Disable], [PasswordTempInd], [ActivationCode]) VALUES (15, N'user abc', N'abc@gmail.com', N'EAAAANgCzs+6w77kziEZMU2xarc2hGNkk6BhsNoybkx5Z2+R', 0, CAST(N'2016-12-03' AS Date), 1, 0, N'EAAAALhMf3iurTnGOhvXWbWHs6WOrysi7C7Evc/PwOT8aSSs')
INSERT [dbo].[User] ([UserId], [UserName], [Email], [Password], [UserType], [CreateDt], [Disable], [PasswordTempInd], [ActivationCode]) VALUES (1015, N'Pepito Juares', N'leobonifazi@gmail.com', N'EAAAAH449ZP2Wb8tRwfy8v20Mp27uTW234+mH9OuvW+ysIUj', 0, CAST(N'2016-12-21' AS Date), 0, 0, N'EAAAAHrUFO5/fIxAROv0iFWlGNytB9mHFL42sgHUHLoisuRsBnqgJ/73Q2tuyuJb6N8VlQ==')
INSERT [dbo].[User] ([UserId], [UserName], [Email], [Password], [UserType], [CreateDt], [Disable], [PasswordTempInd], [ActivationCode]) VALUES (1016, N'user abc', N'abc.com', N'EAAAAC+Qr8pTG17TGVonPO7ZKCSTS6HghCsIXvOd9uTJcggz', 0, CAST(N'2016-12-21' AS Date), 1, 0, N'EAAAAOfck+O2SkS8KGUzgJ+ZUkzXZyQADL8jF385EJZnO1Fa')
SET IDENTITY_INSERT [dbo].[User] OFF
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_Publisher] FOREIGN KEY([PublisherId])
REFERENCES [dbo].[Publisher] ([PublisherID])
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_Publisher]
GO
ALTER TABLE [dbo].[Book_Author]  WITH CHECK ADD  CONSTRAINT [FK_Book_Author_Author] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Author] ([AuthorID])
GO
ALTER TABLE [dbo].[Book_Author] CHECK CONSTRAINT [FK_Book_Author_Author]
GO
ALTER TABLE [dbo].[Book_Author]  WITH CHECK ADD  CONSTRAINT [FK_Book_Author_Book] FOREIGN KEY([BookId])
REFERENCES [dbo].[Book] ([BookId])
GO
ALTER TABLE [dbo].[Book_Author] CHECK CONSTRAINT [FK_Book_Author_Book]
GO
ALTER TABLE [dbo].[Book_Review]  WITH CHECK ADD  CONSTRAINT [FK_Book_Review_Book] FOREIGN KEY([BookId])
REFERENCES [dbo].[Book] ([BookId])
GO
ALTER TABLE [dbo].[Book_Review] CHECK CONSTRAINT [FK_Book_Review_Book]
GO
ALTER TABLE [dbo].[Book_Review]  WITH CHECK ADD  CONSTRAINT [FK_Book_Review_Review] FOREIGN KEY([ReviewId])
REFERENCES [dbo].[Review] ([ReviewId])
GO
ALTER TABLE [dbo].[Book_Review] CHECK CONSTRAINT [FK_Book_Review_Review]
GO
ALTER TABLE [dbo].[Book_Subject]  WITH CHECK ADD  CONSTRAINT [FK_Book_Subject_Book] FOREIGN KEY([BookId])
REFERENCES [dbo].[Book] ([BookId])
GO
ALTER TABLE [dbo].[Book_Subject] CHECK CONSTRAINT [FK_Book_Subject_Book]
GO
ALTER TABLE [dbo].[Book_Subject]  WITH CHECK ADD  CONSTRAINT [FK_Book_Subject_Subject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([SubjectId])
GO
ALTER TABLE [dbo].[Book_Subject] CHECK CONSTRAINT [FK_Book_Subject_Subject]
GO
ALTER TABLE [dbo].[Price]  WITH CHECK ADD  CONSTRAINT [FK_Price_Book] FOREIGN KEY([BookId])
REFERENCES [dbo].[Book] ([BookId])
GO
ALTER TABLE [dbo].[Price] CHECK CONSTRAINT [FK_Price_Book]
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD  CONSTRAINT [FK_Review_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Review] CHECK CONSTRAINT [FK_Review_User]
GO
USE [master]
GO
ALTER DATABASE [SalesBooks] SET  READ_WRITE 
GO
