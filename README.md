# DotnetChallenge21
Challange21 App allows defining list of students, managing their grades and calculating statistics over the grades.

After starting the console app, type HELP and press [RETURN] to see list of available commands.

Accepted grades are in range [1-6]. Grades 1-5 can have an optional '+'.

Data is stored in files.

Example usage:

\>> createstudent Anton

\>> addgrade 4

\>> addgrade 5+

\>> adgrade 3+

\>> addgrade 5

\>> stats
Statistics for student Anton:

Count: 4

Min: 3,5

Max: 5,5

Mean: 4,5

Letter grade: C
