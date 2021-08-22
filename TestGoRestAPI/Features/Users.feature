Feature: UsersFeature

Scenario Outline: Verify user is created
	Given user with email "<Email>" is absent
	When create user:
	| Name   | Email   | Gender   | Status   |
	| <Name> | <Email> | <Gender> | <Status> |
	Then verify user is created
Examples:
| Name        | Email              | Gender | Status   |
| Test User11 | testuser11@test.ts | male   | active   |
| Test User12 | testuser12@test.ts | female | active   |
| Test User13 | testuser13@test.ts | female | active   |

Scenario: Verify user email is updated
	Given user with email "testuser12@test.ts" is present
	When partially update user with email "testuser12@test.ts":
	| Email               |
	| testuser_12@test.ts |
	Then verify user is updated

Scenario: Verify user is updated
	Given user with email "testuser13@test.ts" is present
	When update user with email "testuser13@test.ts":
	| Name        | Email              | Gender | Status   |
	| Test Test14 | testuser14@test.ts | male   | inactive |
	Then verify user is updated

Scenario Outline: Verify user is deleted
	Given user with email "<Email>" is present
	When delete user with email "<Email>"
	Then verify user is deleted
Examples:
| Email               |
| testuser11@test.ts  |
| testuser12@test.ts  |
| testuser_12@test.ts |
| testuser13@test.ts  |
| testuser14@test.ts  |