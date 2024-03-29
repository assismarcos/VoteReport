﻿# VoteReport CLI

# Usage instructions
You can open the project in the Visual Studio, build and run the application. But in that case, the CSV source files must be located in the corresponding bin directory. Otherwise, the application won't work.<br><br>
Another option is to use the command line as follows:
![Usage!](images/01-vote-report-help.png)

You can use the parameter `--files-location` to inform where the CSV source files are located. By doing that, the output files will be generated at the same location.<br>
![Usage!](images/02-vote-report-file-generation.png)

If you try to invoke without the `--files-location` parameter or try to send an empty directory, the application will try to find the CSV files in the EXE directory and generate the output files in this location.<br>
![Usage!](images/03-vote-report-without-parameter.png)<br>
![Usage!](images/04-vote-report-with-parameter-empty-dir.png)

If the source CSV files can't be found in the specified directory, you will see some errors.
![Usage!](images/05-vote-report-wrong-dir.png)
