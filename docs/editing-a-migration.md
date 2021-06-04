# Editing a migration

If the migration file looks wrong or you have missed something, you can do either of the following:

1. While the test database is running in the background, run:

```sh
$ CONNECTION_STRING="Host=127.0.0.1;Database=socialcare;Username=postgres;Password=mypassword;" dotnet ef migrations remove -p ResidentsSocialCarePlatformApi
```

2. Or delete the migration files and revert the changes to `SocialCareContextModelSnapshot.cs`. Make the necessary changes to the context, then create the migration files again.

Note: Any changes made to a `DbSet` or within `SocialCareContext` should have an associated migration generated for it.
