# LCARS

The LCARS project is a dashboard system for anyone who needs an interesting looking interface for their metrics.

The project is designed primarily for a software development team, but could easily be adapted for other purposes.

You need 2 XML files in App_Data that aren't in the repo in order to get the project running. If you implement different content screens, you will not need them for the template.

Settings and Status aren't included in the repo because they contain personal data, but they are required files for certain boards.

The structure is as follows:

Status.xml

	<Tenants>
  		<Tenant ID="1" Name="Tenant 1">
    			<Dependencies>
				<Dependency Name="Dependency 1">
					<Environments>
						<Environment Name="Env 1" Status="OK" />
						<Environment Name="Env 2" Status="OK" />
						<Environment Name="Env 3" Status="v2" />
						<Environment Name="Env 4" Status="v2" />
						<Environment Name="Env 5" Status="v2" />
					</Environments>
				...
				</Dependency>
			...
			</Dependencies>
		</Tenant>
	</Tenants>

Settings.xml (for Jira, Team City and Octopus Deploy integration)

	<Settings>
		<BuildServerCredentials>
			<Username>TC Username</Username>
			<Password>TC Password</Password>
		</BuildServerCredentials>
		<DeploymentServerPath>OD URL</DeploymentServerPath>
		<DeploymentServerKey>OD API KEY</DeploymentServerKey>
		<IssuesUrl>Jira URL</IssuesUrl>
		<IssuesUsername>Jira Username</IssuesUsername>
		<IssuesPassword>Jira Password</IssuesPassword>
	</Settings>
