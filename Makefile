.PHONY: setup
setup:
	docker-compose build

.PHONY: build
build:
	docker-compose build residents-social-care-platform-api

.PHONY: serve
serve:
	docker-compose build residents-social-care-platform-api && docker-compose up residents-social-care-platform-api

.PHONY: shell
shell:
	docker-compose run residents-social-care-platform-api bash

.PHONY: test
test:
	docker-compose build residents-social-care-platform-api-test && docker-compose up residents-social-care-platform-api-test

.PHONY: start-test-db
start-test-db:
	docker-compose up -d test-database

.PHONY: restart-test-db
restart-test-db:
	-container_id=$$(eval docker ps -aqf "name=test-database") \
	-docker kill $$container_id \
	-docker rm $$container_id
	docker-compose up -d test-database

.PHONY: migrate-test-db
migrate-test-db:
	-dotnet tool install -g dotnet-ef
	CONNECTION_STRING="Host=127.0.0.1;Port=6543;Username=postgres;Password=mypassword;Database=socialcare" dotnet ef database update -p ResidentsSocialCarePlatformApi

.PHONY: lint
lint:
	-dotnet tool install -g dotnet-format
	dotnet tool update -g dotnet-format
	dotnet format
