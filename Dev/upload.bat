@echo off
REM 	SE NON FUNZIONA BISOGNA FARE DEI COMANDI:
REM
REM		git config --global user.name="INSERISCI"
REM		git config --global user.email="INSERISCI"
REM
cd ..
set /p "file=File (EMPTY to set everyone): "
if "%file%"=="" git add .

:loop
if "%file%"=="" (
	goto endLoop
)else (
	git add "%file%"
)
set file=
set /p "file=File (EMPTY to end): "
goto loop

:endLoop
git commit
git push
echo Done!