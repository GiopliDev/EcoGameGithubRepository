@echo off
git clone "https://github.com/GiopliDev/EcoGameGithubRepository"
if "%1" neq "" (
	move ./EcoGameGithubRepository ./%1
	goto eof
)
set file=
set /p "folder=Folder Name (EMPTY to get default `EcoGameGithubRepository`): "
if "%folder%" EQU "" (
	move ./EcoGameGithubRepository ./%folder%
	goto eof
)
	