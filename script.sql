USE [TN_CSDLPT]
GO
/****** Object:  User [HTKN]    Script Date: 20/7/2021 12:08:09 PM ******/
CREATE USER [HTKN] FOR LOGIN [HTKN] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  DatabaseRole [MSmerge_56C1BB4F404D496EBFB1CB854AD3AF8D]    Script Date: 20/7/2021 12:08:09 PM ******/
CREATE ROLE [MSmerge_56C1BB4F404D496EBFB1CB854AD3AF8D]
GO
/****** Object:  DatabaseRole [MSmerge_9E52C8DCDBA54EE69AA341AC6BEA7126]    Script Date: 20/7/2021 12:08:09 PM ******/
CREATE ROLE [MSmerge_9E52C8DCDBA54EE69AA341AC6BEA7126]
GO
/****** Object:  DatabaseRole [MSmerge_AC31DB7FF8BF44D9830A89E756256264]    Script Date: 20/7/2021 12:08:09 PM ******/
CREATE ROLE [MSmerge_AC31DB7FF8BF44D9830A89E756256264]
GO
/****** Object:  DatabaseRole [MSmerge_PAL_role]    Script Date: 20/7/2021 12:08:09 PM ******/
CREATE ROLE [MSmerge_PAL_role]
GO
ALTER ROLE [db_owner] ADD MEMBER [HTKN]
GO
ALTER ROLE [MSmerge_PAL_role] ADD MEMBER [MSmerge_56C1BB4F404D496EBFB1CB854AD3AF8D]
GO
ALTER ROLE [MSmerge_PAL_role] ADD MEMBER [MSmerge_9E52C8DCDBA54EE69AA341AC6BEA7126]
GO
ALTER ROLE [MSmerge_PAL_role] ADD MEMBER [MSmerge_AC31DB7FF8BF44D9830A89E756256264]
GO
/****** Object:  Schema [MSmerge_PAL_role]    Script Date: 20/7/2021 12:08:09 PM ******/
CREATE SCHEMA [MSmerge_PAL_role]
GO
/****** Object:  UserDefinedFunction [dbo].[Num2Text]    Script Date: 20/7/2021 12:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[Num2Text](@Number float)
RETURNS nvarchar(100) AS 
BEGIN 
	DECLARE @sNumber nvarchar(100)
	DECLARE @Return	nvarchar(100)
	DECLARE @mDigit varchar
	DECLARE @mLen int
	DECLARE @i int
	DECLARE @mTemp nvarchar(100)
	DECLARE @mNumText nvarchar(100)
	
	SELECT @i = 1
	SELECT @mTemp=''
	
	SELECT @sNumber= CONVERT (VARCHAR(50), @Number,128)
	SELECT @mLen = Len(@sNumber)

	IF @sNumber = '10' SELECT @mTemp=N'mười'
	ELSE
		WHILE @i <= @mLen
		BEGIN
			SELECT @mDigit=SUBSTRING(@sNumber, @i, 1)
			IF @mDigit= '0' SELECT @mNumText=N'không' 
			ELSE
			IF @mDigit= '1' IF @i = 3 SELECT @mNumText=N'mười' ELSE SELECT @mNumText=N'một'
			ELSE
			IF @mDigit= '2' SELECT @mNumText=N'hai'
			ELSE
			IF @mDigit= '3' SELECT @mNumText=N'ba'
			ELSE
			IF @mDigit= '4' SELECT @mNumText=N'bốn'
			ELSE
			IF @mDigit= '5' IF @i = 4 SELECT @mNumText=N'lăm' ELSE SELECT @mNumText=N'năm'
			ELSE
			IF @mDigit= '6' SELECT @mNumText=N'sáu'
			ELSE
			IF @mDigit= '7' SELECT @mNumText=N'bảy'
			ELSE
			IF @mDigit= '8' SELECT @mNumText=N'tám'
			ELSE
			IF @mDigit= '9' SELECT @mNumText=N'chín'
			ELSE
			IF @mDigit= '.' SELECT @mNumText=N'phẩy'
		SELECT @mTemp = @mTemp + ' ' + @mNumText
		IF (@mLen = @i) BREAK
		IF (@i = 3 and @mDigit != '1')
			SELECT @mTemp = @mTemp + ' ' +N'mươi'
		SELECT @i = @i + 1;
		END	
	RETURN @mTemp
END


GO
/****** Object:  Table [dbo].[BANGDIEM]    Script Date: 20/7/2021 12:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BANGDIEM](
	[MASV] [char](8) NOT NULL,
	[MAMH] [char](5) NOT NULL,
	[LAN] [smallint] NOT NULL,
	[NGAYTHI] [datetime] NULL,
	[DIEM] [float] NULL,
	[BAITHI] [ntext] NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL CONSTRAINT [MSmerge_df_rowguid_740D63124157416BB51FBC2336D5CF2A]  DEFAULT (newsequentialid()),
 CONSTRAINT [PK_BANGDIEM] PRIMARY KEY CLUSTERED 
(
	[MASV] ASC,
	[MAMH] ASC,
	[LAN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BODE]    Script Date: 20/7/2021 12:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BODE](
	[CAUHOI] [int] NOT NULL,
	[MAMH] [char](5) NULL,
	[TRINHDO] [char](1) NULL,
	[NOIDUNG] [ntext] NULL,
	[A] [ntext] NULL,
	[B] [ntext] NULL,
	[C] [ntext] NULL,
	[D] [ntext] NULL,
	[DAP_AN] [char](1) NULL,
	[MAGV] [char](8) NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL CONSTRAINT [MSmerge_df_rowguid_85F569B4DABA406BAA664E27B1C39E0B]  DEFAULT (newsequentialid()),
 CONSTRAINT [PK_BODE] PRIMARY KEY CLUSTERED 
(
	[CAUHOI] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[COSO]    Script Date: 20/7/2021 12:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[COSO](
	[MACS] [nchar](3) NOT NULL,
	[TENCS] [nvarchar](50) NOT NULL,
	[DIACHI] [nvarchar](100) NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL CONSTRAINT [MSmerge_df_rowguid_9284FC6A4EAA464EBDCD5F755E0ADD2F]  DEFAULT (newsequentialid()),
 CONSTRAINT [PK_COSO] PRIMARY KEY CLUSTERED 
(
	[MACS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GIAOVIEN]    Script Date: 20/7/2021 12:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GIAOVIEN](
	[MAGV] [char](8) NOT NULL,
	[HO] [varchar](40) NULL,
	[TEN] [varchar](40) NULL,
	[DIACHI] [varchar](50) NULL,
	[MAKH] [nchar](8) NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL CONSTRAINT [MSmerge_df_rowguid_4BA8EC2893E24CC9A4EAA2B10E4D3877]  DEFAULT (newsequentialid()),
 CONSTRAINT [PK_GIAOVIEN] PRIMARY KEY CLUSTERED 
(
	[MAGV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GIAOVIEN_DANGKY]    Script Date: 20/7/2021 12:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GIAOVIEN_DANGKY](
	[MAGV] [char](8) NULL,
	[MAMH] [char](5) NOT NULL,
	[MALOP] [char](8) NOT NULL,
	[TRINHDO] [char](1) NULL,
	[NGAYTHI] [datetime] NULL,
	[LAN] [smallint] NOT NULL,
	[SOCAUTHI] [smallint] NULL,
	[THOIGIAN] [smallint] NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL CONSTRAINT [MSmerge_df_rowguid_575309FCB8D349AC8F2B61E4CD185FC6]  DEFAULT (newsequentialid()),
 CONSTRAINT [PK_GIAOVIEN_DANGKY] PRIMARY KEY CLUSTERED 
(
	[MAMH] ASC,
	[MALOP] ASC,
	[LAN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[KHOA]    Script Date: 20/7/2021 12:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KHOA](
	[MAKH] [nchar](8) NOT NULL,
	[TENKH] [nvarchar](50) NOT NULL,
	[MACS] [nchar](3) NOT NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL CONSTRAINT [MSmerge_df_rowguid_1926011F5C334DCCA7F9FBEE251151DB]  DEFAULT (newsequentialid()),
 CONSTRAINT [PK_KHOA] PRIMARY KEY CLUSTERED 
(
	[MAKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LOP]    Script Date: 20/7/2021 12:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LOP](
	[MALOP] [char](8) NOT NULL,
	[TENLOP] [varchar](40) NOT NULL,
	[MAKH] [nchar](8) NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL CONSTRAINT [MSmerge_df_rowguid_6BEF9BC4A70A463D81374619514B7333]  DEFAULT (newsequentialid()),
 CONSTRAINT [PK_LOP] PRIMARY KEY CLUSTERED 
(
	[MALOP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UN_TENLOP] UNIQUE NONCLUSTERED 
(
	[TENLOP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MONHOC]    Script Date: 20/7/2021 12:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MONHOC](
	[MAMH] [char](5) NOT NULL,
	[TENMH] [varchar](40) NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL CONSTRAINT [MSmerge_df_rowguid_50F0EC0DB35748EFA876F7023BE79BBC]  DEFAULT (newsequentialid()),
 CONSTRAINT [PK_TENMH] PRIMARY KEY CLUSTERED 
(
	[MAMH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UN_TENMH] UNIQUE NONCLUSTERED 
(
	[TENMH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SINHVIEN]    Script Date: 20/7/2021 12:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SINHVIEN](
	[MASV] [char](8) NOT NULL,
	[HO] [varchar](40) NULL,
	[TEN] [varchar](10) NULL,
	[NGAYSINH] [datetime] NULL,
	[DIACHI] [varchar](40) NULL,
	[MALOP] [char](8) NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL CONSTRAINT [MSmerge_df_rowguid_03C4396EBA22434C960C443F543AF985]  DEFAULT (newsequentialid()),
 CONSTRAINT [PK_SINHVIEN] PRIMARY KEY CLUSTERED 
(
	[MASV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[V_DS_PHANMANH]    Script Date: 20/7/2021 12:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_DS_PHANMANH]
AS
SELECT PUBS.description as TENCOSO, SUBS.subscriber_server as TENSERVER
FROM dbo.sysmergepublications PUBS JOIN dbo.sysmergesubscriptions SUBS ON
	PUBS.pubid = SUBS.pubid AND PUBS.publisher <> SUBS.subscriber_server AND PUBS.name<>'TN_CSDLPT_TT'
GO
ALTER TABLE [dbo].[BANGDIEM]  WITH NOCHECK ADD  CONSTRAINT [FK_BANGDIEM_MONHOC] FOREIGN KEY([MAMH])
REFERENCES [dbo].[MONHOC] ([MAMH])
GO
ALTER TABLE [dbo].[BANGDIEM] CHECK CONSTRAINT [FK_BANGDIEM_MONHOC]
GO
ALTER TABLE [dbo].[BANGDIEM]  WITH NOCHECK ADD  CONSTRAINT [FK_BANGDIEM_SINHVIEN1] FOREIGN KEY([MASV])
REFERENCES [dbo].[SINHVIEN] ([MASV])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[BANGDIEM] CHECK CONSTRAINT [FK_BANGDIEM_SINHVIEN1]
GO
ALTER TABLE [dbo].[BODE]  WITH NOCHECK ADD  CONSTRAINT [FK_BODE_GIAOVIEN] FOREIGN KEY([MAGV])
REFERENCES [dbo].[GIAOVIEN] ([MAGV])
GO
ALTER TABLE [dbo].[BODE] CHECK CONSTRAINT [FK_BODE_GIAOVIEN]
GO
ALTER TABLE [dbo].[BODE]  WITH NOCHECK ADD  CONSTRAINT [FK_BODE_MONHOC] FOREIGN KEY([MAMH])
REFERENCES [dbo].[MONHOC] ([MAMH])
GO
ALTER TABLE [dbo].[BODE] CHECK CONSTRAINT [FK_BODE_MONHOC]
GO
ALTER TABLE [dbo].[GIAOVIEN]  WITH CHECK ADD  CONSTRAINT [FK_GIAOVIEN_KHOA] FOREIGN KEY([MAKH])
REFERENCES [dbo].[KHOA] ([MAKH])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[GIAOVIEN] CHECK CONSTRAINT [FK_GIAOVIEN_KHOA]
GO
ALTER TABLE [dbo].[GIAOVIEN_DANGKY]  WITH CHECK ADD  CONSTRAINT [FK_GIAOVIEN_DANGKY_GIAOVIEN1] FOREIGN KEY([MAGV])
REFERENCES [dbo].[GIAOVIEN] ([MAGV])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[GIAOVIEN_DANGKY] CHECK CONSTRAINT [FK_GIAOVIEN_DANGKY_GIAOVIEN1]
GO
ALTER TABLE [dbo].[GIAOVIEN_DANGKY]  WITH CHECK ADD  CONSTRAINT [FK_GIAOVIEN_DANGKY_LOP] FOREIGN KEY([MALOP])
REFERENCES [dbo].[LOP] ([MALOP])
GO
ALTER TABLE [dbo].[GIAOVIEN_DANGKY] CHECK CONSTRAINT [FK_GIAOVIEN_DANGKY_LOP]
GO
ALTER TABLE [dbo].[GIAOVIEN_DANGKY]  WITH CHECK ADD  CONSTRAINT [FK_GIAOVIEN_DANGKY_MONHOC1] FOREIGN KEY([MAMH])
REFERENCES [dbo].[MONHOC] ([MAMH])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[GIAOVIEN_DANGKY] CHECK CONSTRAINT [FK_GIAOVIEN_DANGKY_MONHOC1]
GO
ALTER TABLE [dbo].[KHOA]  WITH CHECK ADD  CONSTRAINT [FK_KHOA_COSO] FOREIGN KEY([MACS])
REFERENCES [dbo].[COSO] ([MACS])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[KHOA] CHECK CONSTRAINT [FK_KHOA_COSO]
GO
ALTER TABLE [dbo].[LOP]  WITH CHECK ADD  CONSTRAINT [FK_LOP_KHOA] FOREIGN KEY([MAKH])
REFERENCES [dbo].[KHOA] ([MAKH])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[LOP] CHECK CONSTRAINT [FK_LOP_KHOA]
GO
ALTER TABLE [dbo].[SINHVIEN]  WITH CHECK ADD  CONSTRAINT [FK_SINHVIEN_LOP] FOREIGN KEY([MALOP])
REFERENCES [dbo].[LOP] ([MALOP])
GO
ALTER TABLE [dbo].[SINHVIEN] CHECK CONSTRAINT [FK_SINHVIEN_LOP]
GO
ALTER TABLE [dbo].[BANGDIEM]  WITH NOCHECK ADD  CONSTRAINT [CK_DIEM] CHECK  (([DIEM]>=(0) AND [DIEM]<=(10)))
GO
ALTER TABLE [dbo].[BANGDIEM] CHECK CONSTRAINT [CK_DIEM]
GO
ALTER TABLE [dbo].[BANGDIEM]  WITH NOCHECK ADD  CONSTRAINT [CK_LANTHI] CHECK  (([LAN]>=(1) AND [LAN]<=(2)))
GO
ALTER TABLE [dbo].[BANGDIEM] CHECK CONSTRAINT [CK_LANTHI]
GO
ALTER TABLE [dbo].[BODE]  WITH NOCHECK ADD  CONSTRAINT [CK_BODE] CHECK  (([TRINHDO]='A' OR [TRINHDO]='B' OR [TRINHDO]='C'))
GO
ALTER TABLE [dbo].[BODE] CHECK CONSTRAINT [CK_BODE]
GO
ALTER TABLE [dbo].[BODE]  WITH NOCHECK ADD  CONSTRAINT [CK_DAPAN] CHECK  (([DAP_AN]='D' OR ([DAP_AN]='C' OR ([DAP_AN]='B' OR [DAP_AN]='A'))))
GO
ALTER TABLE [dbo].[BODE] CHECK CONSTRAINT [CK_DAPAN]
GO
ALTER TABLE [dbo].[GIAOVIEN_DANGKY]  WITH CHECK ADD  CONSTRAINT [CK_LAN] CHECK  (([LAN]>=(1) AND [LAN]<=(2)))
GO
ALTER TABLE [dbo].[GIAOVIEN_DANGKY] CHECK CONSTRAINT [CK_LAN]
GO
ALTER TABLE [dbo].[GIAOVIEN_DANGKY]  WITH CHECK ADD  CONSTRAINT [CK_SOCAUTHI] CHECK  (([SOCAUTHI]>=(10) AND [SOCAUTHI]<=(100)))
GO
ALTER TABLE [dbo].[GIAOVIEN_DANGKY] CHECK CONSTRAINT [CK_SOCAUTHI]
GO
ALTER TABLE [dbo].[GIAOVIEN_DANGKY]  WITH CHECK ADD  CONSTRAINT [CK_THOIGIAN] CHECK  (([THOIGIAN]>=(15) AND [THOIGIAN]<=(60)))
GO
ALTER TABLE [dbo].[GIAOVIEN_DANGKY] CHECK CONSTRAINT [CK_THOIGIAN]
GO
ALTER TABLE [dbo].[GIAOVIEN_DANGKY]  WITH CHECK ADD  CONSTRAINT [CK_TRINHDO] CHECK  (([TRINHDO]='C' OR ([TRINHDO]='B' OR [TRINHDO]='A')))
GO
ALTER TABLE [dbo].[GIAOVIEN_DANGKY] CHECK CONSTRAINT [CK_TRINHDO]
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckClassExists]    Script Date: 20/7/2021 12:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE proc [dbo].[sp_CheckClassExists] @MALOP char(8)
as
begin
	IF NOT EXISTS ( SELECT l.MALOP FROM LINK_SVTT.TN_CSDLPT.dbo.LOP l where l.MALOP = @MALOP)
		BEGIN
			SELECT 0;
			RETURN;
		END
	else 
		SELECT 1;
end




GO
/****** Object:  StoredProcedure [dbo].[sp_CheckCourseExists]    Script Date: 20/7/2021 12:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_CheckCourseExists] @MAMH char(8)
as
begin
 IF EXISTS ( SELECT 1 FROM [MONHOC] mh where mh.MAMH = @MAMH)
	BEGIN
		SELECT 1;
		RETURN;
	END
 ELSE
	SELECT 0;
end
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckDepartmentExists]    Script Date: 20/7/2021 12:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_CheckDepartmentExists] @MAKH char(8)
as
begin
	IF(exists( SELECT k.MAKH FROM TN_CSDLPT.dbo.KHOA as k where k.MAKH = @MAKH))
		BEGIN
			SELECT 1;
			RETURN;
		END
	IF(exists( SELECT k.MAKH FROM LINK.TN_CSDLPT.dbo.KHOA as k where k.MAKH = @MAKH))
		BEGIN
			SELECT 1;
			RETURN;
		END
	SELECT 0;
end
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckQuestionExists]    Script Date: 20/7/2021 12:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_CheckQuestionExists] @CAUHOI int
as
begin
	IF NOT EXISTS ( SELECT bd.CAUHOI FROM BODE bd where bd.CAUHOI = @CAUHOI)
		BEGIN
			SELECT 0;
			RETURN;
		END
	else 
		SELECT 1;
end


GO
/****** Object:  StoredProcedure [dbo].[sp_CheckRegisterIsExists]    Script Date: 20/7/2021 12:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_CheckRegisterIsExists] @MALOP char(8), @MAMH char(8), @LAN int
as
begin
	IF EXISTS( SELECT 1 FROM GIAOVIEN_DANGKY gvdk WHERE gvdk.MALOP = @MALOP AND gvdk.MAMH = @MAMH AND gvdk.LAN = @LAN )
		SELECT 1;
		RETURN;
	IF EXISTS ( SELECT 1 FROM LINK.TN_CSDLPT.dbo.GIAOVIEN_DANGKY gvdk WHERE gvdk.MALOP = @MALOP AND gvdk.MAMH = @MAMH AND gvdk.LAN = @LAN )
		SELECT 1;
		RETURN;
	SELECT 0;
end

GO
/****** Object:  StoredProcedure [dbo].[sp_CheckStudentExists]    Script Date: 20/7/2021 12:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_CheckStudentExists] @MASV char(8)
as
begin
 
 IF NOT EXISTS ( SELECT 1 FROM [LINK_SVTT].[TN_CSDLPT].[dbo].[SINHVIEN] sv where sv.MASV = @MASV)
	BEGIN
		SELECT 0;
		RETURN;
	END
 else 
	SELECT 1;
 
end
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckTecherExists]    Script Date: 20/7/2021 12:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_CheckTecherExists] @MAGV char(8)
as
begin
 
 IF NOT EXISTS ( SELECT 1 FROM GIAOVIEN gv where gv.MAGV = @MAGV)
	BEGIN
		SELECT 0;
		RETURN;
	END
 else 
	SELECT 1;
end
GO
/****** Object:  StoredProcedure [dbo].[sp_GetInfoStudent]    Script Date: 20/7/2021 12:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[sp_GetInfoStudent] @MASV char(8)
as
begin
	Select (sv.HO + ' ' + sv.TEN) as 'FULL NAME', sv.MALOP
	INTO TTSV
	From LINK_SVTT.TN_CSDLPT.dbo.SINHVIEN sv WHERE sv.MASV = @MASV

	Select TTSV.[FULL NAME], TTSV.MALOP, l.TENLOP
	FROM TTSV
	INNER JOIN LINK_SVTT.TN_CSDLPT.dbo.LOP l on TTSV.MALOP = l.MALOP
	
	Drop Table TTSV
end


GO
/****** Object:  StoredProcedure [dbo].[sp_GetListTeacherHadNotAccount]    Script Date: 20/7/2021 12:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_GetListTeacherHadNotAccount]
as
begin
select MAGV,(HO + TEN) as 'HO TEN' 
from  
GIAOVIEN g 
left join 
sys.sysusers  ss
on g.MAGV = ss.name
where ss.uid is null
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getMonHoc]    Script Date: 20/7/2021 12:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create Proc [dbo].[sp_getMonHoc] @maMH char(8)
as
begin
	select mh.TENMH from dbo.MONHOC mh Where mh.MAMH = @maMH 
end


GO
/****** Object:  StoredProcedure [dbo].[sp_GetQuestion]    Script Date: 20/7/2021 12:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_GetQuestion]
@maMonHoc NCHAR(5), @trinhDo NChar(1), @soCau int
AS
BEGIN
	DECLARE @trinhDo2 Nchar(1) = 'N'
	DECLARE @countQuestion int = 0
	DECLARE @countQuestionCungTrinhDo int = 0
	DECLARE @countQuestionTrinhDoDuoi int = 0

	set @countQuestionCungTrinhDo = (select count(*) from BODE where (MAMH = @maMonHoc and TRINHDO = @trinhDo))

	if (@trinhDo = 'A') 
		set @trinhDo2 = 'B'
	else if (@trinhDo = 'B')
		set @trinhDo2 = 'C' 
	
		set @countQuestionTrinhDoDuoi = (select count(*) from BODE where (MAMH = @maMonHoc and TRINHDO = @trinhDo2))
		set @countQuestion = @countQuestionCungTrinhDo+@countQuestionTrinhDoDuoi
		
		--set @countQuestion = @countQuestionCungTrinhDo

	if(@countQuestion <@soCau or @countQuestionCungTrinhDo<(@soCau*70/100))
		begin
			Select('Khong du cau hoi de thi') as 'ERROR'
			return -1
		end
	
	-- tạo bảng tạm chưa các câu hỏi tại tất cả các site theo input ban đầu
	create table AtSiteTable(
	CAUHOI int primary key,
	TRINHDO char(1),
	NOIDUNG ntext,
	A ntext,
	B ntext,
	C ntext,
	D ntext,
	DAP_AN nchar(1),
	)

	select * into CungTrinhDoAtSiteTable from BODE  where MAMH = @maMonHoc and TRINHDO = @trinhDo and MAGV in (Select MAGV from GIAOVIEN where MAKH in(select MAKH from KHOA))
	select * into TrinhDoDuoiAtSiteTable from BODE  where MAMH = @maMonHoc and TRINHDO = @trinhDo2 and MAGV in (Select MAGV from GIAOVIEN where MAKH in(select MAKH from KHOA))
	
	Insert into AtSiteTable
			select CAUHOI,TRINHDO,CAST(NOIDUNG as nvarchar(max)),CAST(A as nvarchar(max)),CAST(B as nvarchar(max)),CAST(C as nvarchar(max)),
			CAST(D as nvarchar(max)),DAP_AN from CungTrinhDoAtSiteTable
			UNION
			select CAUHOI,TRINHDO,CAST(NOIDUNG as nvarchar(max)),CAST(A as nvarchar(max)),CAST(B as nvarchar(max)),CAST(C as nvarchar(max)),
			CAST(D as nvarchar(max)),DAP_AN  from TrinhDoDuoiAtSiteTable 
	
	Declare @countCungTrinhDoAtSite int = (select count(*) from CungTrinhDoAtSiteTable)
	Declare @countTrinhDoDuoiAtSite int = (select count(*) from TrinhDoDuoiAtSiteTable)
	if((@countCungTrinhDoAtSite+@countTrinhDoDuoiAtSite) < @soCau OR @countCungTrinhDoAtSite < (@soCau*70/100))
		begin
			print 'here'
			select * into CungTrinhDoOtherSiteTable from BODE  where MAMH = @maMonHoc and TRINHDO = @trinhDo and MAGV in (Select MAGV from GIAOVIEN where MAKH not in(select MAKH from KHOA))
			select * into TrinhDoDuoiOtherSiteTable from BODE  where MAMH = @maMonHoc and TRINHDO = @trinhDo2 and MAGV in (Select MAGV from GIAOVIEN where MAKH not in(select MAKH from KHOA))

			Insert into AtSiteTable
			select CAUHOI,TRINHDO,CAST(NOIDUNG as nvarchar(max)),CAST(A as nvarchar(max)),CAST(B as nvarchar(max)),CAST(C as nvarchar(max)),
			CAST(D as nvarchar(max)),DAP_AN from CungTrinhDoOtherSiteTable
			UNION
			select CAUHOI,TRINHDO,CAST(NOIDUNG as nvarchar(max)),CAST(A as nvarchar(max)),CAST(B as nvarchar(max)),CAST(C as nvarchar(max)),
			CAST(D as nvarchar(max)),DAP_AN  from TrinhDoDuoiOtherSiteTable 
			drop table CungTrinhDoOtherSiteTable
			drop table TrinhDoDuoiOtherSiteTable
		end
	Declare @slCanLayCungTrinhDo int,@slCanLayTrinhDoDuoi int
		set @slCanLayCungTrinhDo = @soCau*70/100
		if ((@slCanLayCungTrinhDo + @countQuestionTrinhDoDuoi)<@soCau)
			set @slCanLayCungTrinhDo = @slCanLayCungTrinhDo + (@soCau - (@slCanLayCungTrinhDo+@countQuestionTrinhDoDuoi))
			set @slCanLayTrinhDoDuoi = @soCau - @slCanLayCungTrinhDo
		
	select top (@slCanLayCungTrinhDo) * into temp1 from AtSiteTable where TRINHDO = @trinhDo
	ORDER BY NEWID()
	
	select top (@slCanLayTrinhDoDuoi) * into temp2 from AtSiteTable where TRINHDO = @trinhDo2
	ORDER BY NEWID()

	select CAUHOI,CAST(NOIDUNG as nvarchar(max)) as [NOIDUNG],CAST(A as nvarchar(max)) as [A],CAST(B as nvarchar(max)) as [B],CAST(C as nvarchar(max)) as [C],
	CAST(D as nvarchar(max)) as [D],DAP_AN  from temp1
	UNION
	select CAUHOI,CAST(NOIDUNG as nvarchar(max)) as [NOIDUNG],CAST(A as nvarchar(max)) as [A],CAST(B as nvarchar(max)) as [B],CAST(C as nvarchar(max)) as [C],
	CAST(D as nvarchar(max)) as [D],DAP_AN  from temp2

	drop table AtSiteTable
	drop table CungTrinhDoAtSiteTable
	drop table TrinhDoDuoiAtSiteTable
	drop table temp1
	drop table temp2
	--
	
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetReport]    Script Date: 20/7/2021 12:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_GetReport] @MACS varchar(3), @FROM date, @TO date
as
begin

create table #tbl_temp(
					MALOP char(8)
					,TENLOP varchar(40)
					,MAMH char(8)
					,LAN smallint
				)
create table #tbl_temp2(
					MALOP char(8)
					,TENLOP varchar(40)
					,MAGV char(8)
					,MAMH char(8)
					,LAN smallint
				    ,NGAYTHI date
					,SOCAUTHI smallint
					,DATHI bit
					)			

--Lấy danh sách các lượt đăng ký thi theo cơ sở		
insert into #tbl_temp(MALOP, TENLOP, MAMH, LAN)
select ll.MALOP, ll.TENLOP, gvdk.MAMH, gvdk.LAN
from
	(select l.MALOP, l.TENLOP, l.MAKH 
	from
		TN_CSDLPT.dbo.KHOA k, TN_CSDLPT.dbo.LOP l
	where (k.MACS = @MACS AND l.MAKH = k.MAKH)
	) as ll, TN_CSDLPT.dbo.GIAOVIEN_DANGKY gvdk
where ((gvdk.NGAYTHI Between @FROM AND @TO) AND ll.MALOP = gvdk.MALOP) 

--Đếm số lượt đã thi thông qua table bảng điểm
select lsv.TENLOP, lsv.MALOP, lsv.MAMH, lsv. LAN, DATHI = (case when COUNT(bd.MASV) > 0 then 'X' else '' end) 
into #tbl_temp3
from
(
	select t.TENLOP, t.MALOP, t.MAMH, t.LAN, sv.MASV 
	from 
	(
		#tbl_temp t left join TN_CSDLPT.dbo.SINHVIEN sv
		on t.MALOP = sv.MALOP
	)
)as lsv
left join
TN_CSDLPT.dbo.BANGDIEM bd
on bd.MASV = lsv.MASV And bd.MAMH = lsv.MAMH And bd.LAN = lsv.LAN
group by lsv.MALOP, lsv.TENLOP, lsv.MAMH, lsv.LAN

--Lấy thêm thông tin cho báo cáo
select t.TENLOP, t.MAMH, gvdk.MAGV, gvdk.SOCAUTHI, t.LAN, gvdk.NGAYTHI, t.DATHI
into #tbl_Info
from 
#tbl_temp3 t
inner join 
TN_CSDLPT.dbo.GIAOVIEN_DANGKY gvdk
on (t.MALOP = gvdk.MALOP And t.MAMH = gvdk.MAMH And t.LAN = gvdk.LAN)

--Sửa các mã thành tên
select t.TENLOP, rs1.TENMH, ftt.HOTEN, t.SOCAUTHI,t.LAN, t.NGAYTHI, t.DATHI
from 
(
#tbl_Info t
inner join 
(select mh.MAMH, mh.TENMH from TN_CSDLPT.dbo.MONHOC mh 
 where mh.MAMH in (select ti.MAMH from #tbl_Info ti)) as rs1
on t.MAMH = rs1.MAMH
)
inner join 
(select gv.MAGV, (gv.HO + ' ' +gv.TEN) as 'HOTEN' from TN_CSDLPT.dbo.GIAOVIEN gv 
 where gv.MAGV in (select ti.MAGV from #tbl_Info ti)) as ftt
on t.MAGV = ftt.MAGV

drop table #tbl_temp
drop table #tbl_temp2
drop table #tbl_temp3
drop table #tbl_Info

end



GO
/****** Object:  StoredProcedure [dbo].[sp_getTenKhoa]    Script Date: 20/7/2021 12:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [dbo].[sp_getTenKhoa] @maKhoa char(8)
as
begin
	select k.TENKH from dbo.KHOA k Where k.MAKH = @maKhoa 
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getTenLop]    Script Date: 20/7/2021 12:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create Proc [dbo].[sp_getTenLop] @maLop char(8)
as
begin
	select l.TENLOP from dbo.LOP l Where l.MALOP = @maLop 
end
GO
/****** Object:  StoredProcedure [dbo].[sp_GetTranscript]    Script Date: 20/7/2021 12:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[sp_GetTranscript] @MALOP char(8), @MAMH char(8), @LAN int
as
begin
	Select lsv.MASV, [HO VA TEN], DIEM, dbo.Num2Text(DIEM) as 'DIEM CHU'
	From
		(Select sv.MASV, (sv.HO + ' ' + sv.TEN) as 'HO VA TEN'  
		 From SINHVIEN sv
		 Where (sv.MALOP = @MALOP)) As lsv
	   Inner Join
		(Select bd.MASV, bd.DIEM
		 From  BANGDIEM bd
		 Where (bd.MAMH = @MAMH AND bd.LAN = @LAN )) As bdl 
	   On lsv.MASV = bdl.MASV
end


GO
/****** Object:  StoredProcedure [dbo].[sp_HasBeenExamined]    Script Date: 20/7/2021 12:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_HasBeenExamined] @MASV char(8), @MAMH char(8), @LAN int
as
begin
	IF NOT EXISTS(Select bd.MASV from BANGDIEM bd where (bd.MASV = @MASV AND bd.MAMH = @MAMH AND bd.LAN = @LAN))
		BEGIN
			SELECT 0;
			RETURN;
		END
	ELSE 
		SELECT 1;
end

GO
/****** Object:  StoredProcedure [dbo].[sp_LogOut]    Script Date: 20/7/2021 12:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_LogOut] @loginName char(50)
as
begin
DECLARE @kill varchar(8000) = '';  
SELECT @kill = @kill + 'kill ' + CONVERT(varchar(5), session_id) + ';'  
FROM sys.dm_exec_sessions
WHERE login_name = @loginName

EXEC(@kill);
end
GO
/****** Object:  StoredProcedure [dbo].[sp_RegisterExamed]    Script Date: 20/7/2021 12:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_RegisterExamed] @MALOP char(8), @MAMH char(8), @LAN int
as
begin
	select DATHI = (case when COUNT(msv.MASV) > 0 then 'X' else '' end) from
	(SELECT bd.MASV FROM BANGDIEM bd WHERE bd.MAMH = @MAMH AND bd.LAN = @LAN)as msv
	left join TN_CSDLPT.dbo.SINHVIEN sv
	on msv.MASV = sv.MASV and sv.MALOP = @MALOP
end

GO
/****** Object:  StoredProcedure [dbo].[sp_TaoTaiKhoan]    Script Date: 20/7/2021 12:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_TaoTaiKhoan]
	@LGNAME VARCHAR(50),
	@PASS VARCHAR(50),
	@USERNAME VARCHAR(50),
	@ROLE VARCHAR(50)
AS
BEGIN
  DECLARE @RET INT
  EXEC @RET= SP_ADDLOGIN @LGNAME, @PASS,'TN_CSDLPT'                     

  IF (@RET =1)  -- LOGIN NAME BI TRUNG
     RETURN 1

  EXEC @RET= SP_GRANTDBACCESS @LGNAME, @USERNAME
  IF (@RET =1)  -- USER  NAME BI TRUNG
  BEGIN
       EXEC SP_DROPLOGIN @LGNAME
       RETURN 2
  END
  EXEC sp_addrolemember @ROLE, @USERNAME

  IF @ROLE= 'TRUONG' 
	BEGIN
		EXEC sp_addsrvrolemember @LGNAME, 'sysadmin'
		EXEC sp_addsrvrolemember @LGNAME, 'SecurityAdmin'
		EXEC sp_addsrvrolemember @LGNAME, 'ProcessAdmin'
	END

  IF @ROLE= 'COSO'
	BEGIN 
		EXEC sp_addsrvrolemember @LGNAME, 'sysadmin'
		EXEC sp_addsrvrolemember @LGNAME, 'SecurityAdmin'
		EXEC sp_addsrvrolemember @LGNAME, 'ProcessAdmin'
	END
  IF @ROLE= 'GIANGVIEN'
	BEGIN  
		EXEC sp_addsrvrolemember @LGNAME, 'sysadmin'
		EXEC sp_addsrvrolemember @LGNAME, 'SecurityAdmin'
		EXEC sp_addsrvrolemember @LGNAME, 'ProcessAdmin'
	END
  IF @ROLE= 'SINHVIEN'
	BEGIN  
		EXEC sp_addsrvrolemember @LGNAME, 'ProcessAdmin'
	END

END

GO
/****** Object:  StoredProcedure [dbo].[SP_THONGTINDANGNHAPGV]    Script Date: 20/7/2021 12:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_THONGTINDANGNHAPGV]
@TENLOGIN NVARCHAR (50)
AS
DECLARE @TENUSER NVARCHAR(50)
SELECT @TENUSER=NAME FROM sys.sysusers WHERE sid = SUSER_SID(@TENLOGIN)
 SELECT USERNAME = @TENUSER, 
  HOTEN = (SELECT HO+ ' '+ TEN FROM GIAOVIEN  WHERE MAGV = @TENUSER ),
   NAME
   FROM sys.sysusers 
   WHERE UID = (SELECT GROUPUID 
                 FROM SYS.SYSMEMBERS 
               WHERE MEMBERUID= (SELECT UID FROM sys.sysusers  WHERE NAME=@TENUSER))

GO
/****** Object:  StoredProcedure [dbo].[SP_THONGTINDANGNHAPSV]    Script Date: 20/7/2021 12:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[SP_THONGTINDANGNHAPSV] @MASV NVARCHAR (50)
AS
DECLARE @TENUSER NVARCHAR(50)
SELECT @TENUSER=NAME FROM sys.sysusers WHERE sid = SUSER_SID('SV')
SELECT USERNAME = @TENUSER, 
  HOTEN = (SELECT HO+ ' '+ TEN FROM SINHVIEN  WHERE MASV = @MASV ),
   NAME
   FROM sys.sysusers 
   WHERE UID = (SELECT GROUPUID 
                 FROM SYS.SYSMEMBERS 
               WHERE MEMBERUID= (SELECT UID FROM sys.sysusers  WHERE NAME=@TENUSER))


GO
