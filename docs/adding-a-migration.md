# Adding a migration

1. You need have the [dotnet ef CLI tool](https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet) installed.
   To install run:

```sh
$ dotnet tool install --global dotnet-ef
```

2. Make the necessary changes to the database model i.e `SocialCareContext` and/or any of the DbSet's listed in the file.

3. In your terminal, navigate to the root folder of the repo and run:

```sh
$ dotnet ef migrations add -o ./V1/Infrastructure/Migrations -p ResidentsSocialCarePlatformApi NameOfThisMigration
```

`NameOfThisMigration` should be replaced with your migration name e.g. AddColumnNameToPeopleTable.
This will create a Migrations folder in the Infrastructure (if it doesn't exist)
and creates a migration for the changes you've made for the database model.

4. Go to the folder /ResidentContactApi/V1/Infrastructure/Migrations and you should see two new files for the migration.
   In the one which doesn't end in `.Designer` you can check through the migration script to make sure the migration file has the changes you expect.
