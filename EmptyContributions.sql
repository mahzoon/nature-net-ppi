USE [nature-net-test]
GO
delete from Action
where id > 0
delete from Collection_Contribution_Mapping
where id > 0
delete from Collection
where id > 0
delete from Contribution
where id > 0
delete from Feedback
where id > 0
delete from [dbo].[User]
where id > 0
delete from [dbo].[Interaction_Log]
where id > 0
