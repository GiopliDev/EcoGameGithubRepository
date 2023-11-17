@echo off
cd ..
set file=
set /p "file=File (EMPTY to end): "
if "%file%" EQU "" (
	goto eof
)

:loop
if "%file%" NEQ "" (
	git rm "%file%"
) else (
	goto end
)
set file=
set /p "file=File (EMPTY to end): "
goto loop
:end
git commit
git push