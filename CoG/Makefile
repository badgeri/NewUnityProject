
UNITY=/c/Program\ Files/Unity/Editor/Unity.exe
PATH = `pwd`

echo: force
	echo ${PATH}
build: force
	${UNITY} -batchmode -quit -projectPath ${PATH} -executeMethod BuildCommand.BuildWin64

runTests: force
	${UNITY} -batchmode -quit -projectPath ${PATH} -batchmode -runTests -testPlatform playmode -testResults ${PATH}/testresults.xml -logFile ${PATH}/logfile_tests.txt

force:
	
