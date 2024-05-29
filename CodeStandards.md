# Coding Standards
## C++
### Naming conventions

These are taken from Microsoft guidelines
| Name Type | Format | Comment |
| --- | --- | --- |
| Namespace | under_scored |  |
| Class Name | PascalCase | do not use C or T prefix |
| Function name	| camelCase	| Lower case start |
| Parameters/Locals |	under_scored | 	Vast majority of standards recommends this because _ is more readable to C++ crowd |
| Member variable | under_scored_with |The prefix _ is heavily discourage |
| Enums and its members |	CamelCase	Most except very old standards agree with this one |
| Globals | g_under_scored |You shouldn't have these in first place! |
| Constants | UPPER_CASE | |
| File names | Match case of class name |

### Include files
include files should be in the order of 
1. Include file for that source file
2. C system files
3. C++ system files
4. A blank line
5. needed library .h files
6. A blank line
7. project .h files
Forward declaration is desirable over nested include files.
Every .h file should have a **#ifndef ... #define ... #endif** to protect for mulitiple inclusions.

## Python

### Naming Conventions
Modules should have all lower case names
Class names should be CamelCase
funcion names should be lowercase.
Constants should be UPPER_CASE
