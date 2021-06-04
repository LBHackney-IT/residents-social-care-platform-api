# Troubleshooting migrations

If you encounter errors about tables already existing after running the `migrate-test-db` make command,
you can try using [psql](https://www.postgresql.org/docs/current/app-psql.html)
to delete all the existing tables in `dbo` schema and the `__EFMigrationsHistory` table from the `socialcare` database hosted on your localhost.

1. In your terminal, connect to the localhost instance of socialcare using psql:

```sh
$ psql -h 127.0.0.1 -p 5432 -d socialcare -U postgres
```

2. Delete all the tables in the socialcare `dbo` schema:

```sh
DROP SCHEMA IF EXISTS dbo CASCADE;
```
N.B: When running the migrations, it will check if the `dbo` schema exists first and create it if doesn't, so no need to recreate it here.

3. Delete the EF Migrations History table:
```sh
DROP TABLE __EFMigrationsHistory CASCADE;
```

N.B: Do not delete the public schema, if you do, you will need to recreate it using `CREATE SCHEMA`.

4. Exit the psql. You can use `\q`
5. Run the `make migrate-test-db` command again. This should run the migrations on the database, create a new `__EFMigrationsHistory` table and save the new migration history there.
