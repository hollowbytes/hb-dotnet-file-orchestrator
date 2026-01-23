INSERT INTO [dbo].[StorageRule] ( [Name], [Expression])
VALUES ('Default', 'metadata.Form.Any(x => x.Key == "customer")');

GO;

INSERT INTO [dbo].[FileSystemStorage] ( [RuleId], [Name], [Destination])
VALUES (1, 'Default', './customers/{{ metadata.Form.customer[0] }}');