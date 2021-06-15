$CleanCommand = "docker rm -f houseplants";
$BuildCommand = "docker build -t houseplants:dev -f .\Application\HousePlants\Dockerfile .\Application\HousePlants\";
$RunCommand = "docker run -d --name houseplants " +
			  "--network postgres-network " +
			  "-p 30080:80 " + 
			  "-p 30443:443 " +
			  "-e ASPNETCORE_ENVIRONMENT=Docker " +
			   "houseplants:dev";

Invoke-Expression $CleanCommand
Invoke-Expression $BuildCommand
Invoke-Expression $RunCommand
