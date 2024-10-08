USE [PostComments]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 25/09/2024 02:10:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[isActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 25/09/2024 02:10:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[CommentId] [int] IDENTITY(1,1) NOT NULL,
	[PostId] [int] NOT NULL,
	[Content] [varchar](300) NULL,
	[CreatedAt] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PostCategories]    Script Date: 25/09/2024 02:10:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostCategories](
	[PostId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PostId] ASC,
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 25/09/2024 02:10:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[PostId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](100) NOT NULL,
	[Content] [varchar](200) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Categories] ADD  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[Comments] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Posts] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Posts] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD FOREIGN KEY([PostId])
REFERENCES [dbo].[Posts] ([PostId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PostCategories]  WITH CHECK ADD FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PostCategories]  WITH CHECK ADD FOREIGN KEY([PostId])
REFERENCES [dbo].[Posts] ([PostId])
ON DELETE CASCADE
GO
/****** Object:  StoredProcedure [dbo].[sp_añadirCategoriesPost]    Script Date: 25/09/2024 02:10:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_añadirCategoriesPost]
	@PostId int,
	@Categorias nvarchar(max)
as
begin
    IF EXISTS (SELECT 1 FROM PostCategories WHERE PostId = @PostId)
    BEGIN
        DELETE FROM PostCategories WHERE PostId = @PostId;
    END

    INSERT INTO PostCategories (PostId, CategoryId)
    SELECT @PostId, value
    FROM STRING_SPLIT(@Categorias, ',');
end;
GO
/****** Object:  StoredProcedure [dbo].[sp_crearCategory]    Script Date: 25/09/2024 02:10:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_crearCategory]
	@Name nvarchar(50)
as
begin
	insert into Categories (Name)
	values(@Name)
end;
GO
/****** Object:  StoredProcedure [dbo].[sp_crearComment]    Script Date: 25/09/2024 02:10:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_crearComment]
	@PostId int,
	@Content varchar(200),
	@CommentId int output
as
begin
	insert into Comments (PostId, Content, CreatedAt)
	values (@PostId, @Content, GETDATE());

    SET @CommentId = SCOPE_IDENTITY();
end; 
GO
/****** Object:  StoredProcedure [dbo].[sp_crearPosts]    Script Date: 25/09/2024 02:10:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_crearPosts]
	@Title varchar(100),
	@Content varchar(200)
as
begin
	insert into Posts(Title, Content, CreatedAt, UpdatedAt)
	values (@Title, @Content, GETDATE(), GETDATE());
end 
GO
/****** Object:  StoredProcedure [dbo].[sp_detallesPost]    Script Date: 25/09/2024 02:10:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_detallesPost]
	@PostId int
as
begin
	select *
	from Posts
	where PostId = @PostId;

	select pc.PostId ,c.CategoryId, c.Name 
	from Categories c
	inner join PostCategories pc on c.CategoryId = pc.CategoryId
	where pc.PostId = @PostId and c.isActive = 1;

	select co.CommentId, co.Content, co.CreatedAt, co.PostId
	from Comments co
	where co.PostId = @PostId;
end
GO
/****** Object:  StoredProcedure [dbo].[sp_editarCategory]    Script Date: 25/09/2024 02:10:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_editarCategory]
	@CategoryId int,
	@Name nvarchar(50)
as
begin
	update Categories
	set Name = @Name
	where CategoryId = @CategoryId;
end
GO
/****** Object:  StoredProcedure [dbo].[sp_editarComment]    Script Date: 25/09/2024 02:10:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_editarComment]
	@CommentId int,
	@Content varchar(200)
as
begin
	update Comments
	set Content = @Content,
		CreatedAt = GETDATE()
	where CommentId = @CommentId;
end;
GO
/****** Object:  StoredProcedure [dbo].[sp_editarPosts]    Script Date: 25/09/2024 02:10:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_editarPosts]
	@PostId int,
	@Title nvarchar(100),
	@Content nvarchar(max)
as
begin
	update Posts
	set Title = @Title,
		Content = @Content,
		UpdatedAt = GETDATE()
	where PostId = @PostId;
end;
GO
/****** Object:  StoredProcedure [dbo].[sp_eliminarCategory]    Script Date: 25/09/2024 02:10:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_eliminarCategory]
	@CategoryId int
as
begin
	update Categories
	set isActive = 0
	where CategoryId = @CategoryId;
end;
GO
/****** Object:  StoredProcedure [dbo].[sp_eliminarComment]    Script Date: 25/09/2024 02:10:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_eliminarComment]
	@CommentId int
as
begin
	delete from Comments
	where CommentId = @CommentId;
end;
GO
/****** Object:  StoredProcedure [dbo].[sp_eliminarPosts]    Script Date: 25/09/2024 02:10:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_eliminarPosts]
	@PostId int
as
begin
	delete from PostCategories
	where PostId = @PostId;
	
	delete from Comments
	where PostId = @PostId;

	delete from Posts 
	where PostId = @PostId;
end;
GO
/****** Object:  StoredProcedure [dbo].[sp_listaPosts]    Script Date: 25/09/2024 02:10:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_listaPosts]
	@Page INT,
    @PageSize INT
as
begin
    SELECT PostId, Title, Content, CreatedAt
    FROM Posts
    ORDER BY CreatedAt DESC
    OFFSET (@Page - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

	SELECT COUNT(*) AS TotalPosts
	From Posts
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_listarCategories]    Script Date: 25/09/2024 02:10:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_listarCategories]
as
begin
	select * 
	from Categories where isActive = 1;
end
GO
