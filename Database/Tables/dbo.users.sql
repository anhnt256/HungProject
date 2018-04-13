CREATE TABLE [dbo].[users]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[language_id] [int] NULL,
[address_id] [int] NULL,
[username] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[email] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[password] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[first_name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[middle_name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[last_name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[full_name] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[is_active] [bit] NULL,
[is_delete] [bit] NULL,
[created_by] [int] NULL,
[updated_by] [int] NULL,
[created_on] [datetime] NULL,
[updated_on] [datetime] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[users] ADD CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED  ([id]) ON [PRIMARY]
GO
