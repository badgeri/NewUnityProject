UNITY=/c/Program\ Files/Unity/Editor/Unity.exe
PATH = `pwd`

build: force
	${UNITY} -batchmode -quit -projectPath ${PATH} -executeMethod BuildCommand.BuildAll

runTests: playmode editmode checklogs
	
playmode:
	- ${UNITY} -batchmode -projectPath ${PATH} -runTests -testPlatform playmode -testResults ${PATH}/Build/log/testresults_playmode.xml -logFile ${PATH}/Build/log/logfile_playmode.txt

editmode:
	- ${UNITY} -batchmode -projectPath ${PATH} -runEditorTests -editorTestsResultFile ${PATH}/Build/log/testresults_editormode.xml -logFile ${PATH}/Build/log/logfile_editormode.txt

checklogs:
	/usr/bin/grep.exe -e "failed=\"[1-9]" ${PATH}/Build/log/testresults_*mode.xml | echo "Failing tests" && exit 1 || /usr/bin/echo success
