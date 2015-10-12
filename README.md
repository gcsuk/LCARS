# LCARS

The LCARS project is a dashboard system for anyone who needs an interesting looking interface for their metrics.

The project is designed primarily for a software development team, but could easily be adapted for other purposes.

You need 3 XML files in App_Data in order to get the project running. If you implement different content screens, you will not need them for the template.

Settings, Deployments and Status aren't included on GitHub as they contain personal data, but they are required files

The structure is as follows:

Status.xml

&lt;Tenants&gt;
  &lt;Tenant ID="1" Name="Tenant 1"&gt;
    &lt;Dependencies&gt;
      &lt;Dependency Name="Dependency 1"&gt;
        &lt;Environments&gt;
          &lt;Environment Name="Env 1" Status="OK" /&gt;
          &lt;Environment Name="Env 2" Status="OK" /&gt;
          &lt;Environment Name="Env 3" Status="v2" /&gt;
          &lt;Environment Name="Env 4" Status="v2" /&gt;
          &lt;Environment Name="Env 5" Status="v2" /&gt;
        &lt;/Environments&gt;
	...
      &lt;/Dependency&gt;
      ...
    &lt;/Dependencies&gt;
  &lt;/Tenant&gt;
&lt;/Tenants&gt;

Settings.xml (for Team City and Octopus Deploy integration)

&lt;Settings&gt;
  &lt;BuildServerCredentials&gt;
    &lt;Username&gt;Username&lt;/Username&gt;
    &lt;Password&gt;Password&lt;/Password&gt;
  &lt;/BuildServerCredentials&gt;
  &lt;DeploymentServerPath&gt;URL&lt;/DeploymentServerPath&gt;
&lt;/Settings&gt;
