.PHONY: setup
setup:
	docker-compose build

.PHONY: build
build:
	docker-compose build mosaic-resident-information-api

.PHONY: serve
serve:
	docker-compose build mosaic-resident-information-api && docker-compose up mosaic-resident-information-api

.PHONY: shell
shell:
	docker-compose run mosaic-resident-information-api bash

.PHONY: test
test:
	docker-compose build mosaic-resident-information-api-test && docker-compose up mosaic-resident-information-api-test

.PHONY: lint
lint:
	-dotnet tool install -g dotnet-format
	dotnet tool update -g dotnet-format
	dotnet format
