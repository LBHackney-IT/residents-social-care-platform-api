from diagrams import Cluster, Diagram
from diagrams.aws.compute import Lambda
from diagrams.aws.mobile import APIGateway
from diagrams.aws.database import RDSPostgresqlInstance
from diagrams.programming.language import Csharp

graph_attr = {
    "pad": "0.5",
}

with Diagram("Residents Social Care Platform API", show=True, graph_attr=graph_attr, filename="architecture"):
    with Cluster("AWS Account: Mosaic-Production"):
        awsAPIGateway = APIGateway("\nAPI Gateway")
        awsLambda = Lambda("\nLambda")
        cSharpApp = Csharp("\n.NET Core App")
        postgresDb = RDSPostgresqlInstance("\nDatabase")

        awsAPIGateway >> awsLambda >> cSharpApp >> postgresDb
