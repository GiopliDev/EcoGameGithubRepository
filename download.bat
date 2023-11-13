@echo off
git clone "https://github.com/GiopliDev/EcoGameGithubRepository"
if "%1" neq "" (
	move ./EcoGameGithubRepository ./%1
)