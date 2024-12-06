# MultiThreadFileAccess Application #
The MultiThreadFileAccess application demonstrates concurrent updates of a file in multithreaded environment.

## File Structure ##
 ### MultiThreadFileAccess Folder ### 
 This folder consists of coding files, project, tests and solution files.
 ### Results Folder ###
 This folder consists of DeveloperNotes and sample results file .

## Usage ##

### Windows execution ###
1. Open a Command Prompt or PowerShell window.
2. Navigate to the directory containing the compiled MultiThreadFileAccess application
3. Run the MultiThreadFileAccess.exe
4. File is generated in c:/junk

### Docker image exectuion ###
1.Pull docker image 
   ```
   docker pull deepaliap/multithreadfileaccessapp:latest 
   ```
2.Execute the docker command 
   ```
   docker run -i -v c:/junk:/log deepaliap/multithreadfileaccessapp 
   ```

3. File is generated in c:/junk
