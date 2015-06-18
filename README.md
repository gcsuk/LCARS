# LCARS

The LCARS project is a dashboard system for anyone who needs an interesting looking interface for their metrics.

The project is designed primarily for a software development team, but could easily be adapted for other purposes.

You need 2 XML files in App_Data in order to get the project running. If you implement different content screens, you wilol not need them for the template.

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


AutoDeploy.xml

<AutoDeploy>
  <IsEnabled>0</IsEnabled>
  <TargetDate>June 1, 2050 00:00:00</TargetDate>
</AutoDeploy>