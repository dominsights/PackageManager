Feature: InstallPackage

Client install package

@tag1
Scenario: Install package without dependencies
	Given the selected package is "notepadplusplus"
	When install command is sent
	Then the installation status should be "installed"
