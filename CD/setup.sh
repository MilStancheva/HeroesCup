apt-get install mariadb-server
sudo mysql_secure_installation 
#https://stackoverflow.com/questions/42043205/how-to-fix-mysql-index-column-size-too-large-laravel-migrate
apt-get install nginx
sudo add-apt-repository universe
sudo apt-get update
sudo apt-get install apt-transport-https
sudo apt-get update
sudo apt-get install aspnetcore-runtime-3.1
useradd actions-runner
passwd actions-runner
cd /var
mkdir actions-runner && cd actions-runner
chown -R actions-runner /var/actions-runner/
sudo usermod -a -G sudo actions-runner
sudo chsh actions-runner -s /bin/bash
sudo usermod -a -G www-data actions-runner 
sudo chown -R :www-data /var/www/
su - actions-runner
cd /var/actions-runner
curl -O -L https://github.com/actions/runner/releases/download/v2.262.1/actions-runner-linux-x64-2.262.1.tar.gz
tar xzf ./actions-runner-linux-x64-2.262.1.tar.gz
./config.sh --url https://github.com/MilStancheva/HeroesCup --token xxx
./svc.sh

# 2014  export HEROESCUP_CONNECTIONSTRING="server=localhost;port=3306;database=heroes_cup_db;uid=heroescup;password=xxx"
#https://stackoverflow.com/questions/34212765/how-do-i-get-the-kestrel-web-server-to-listen-to-non-localhost-requests
#https://askubuntu.com/questions/692701/allowing-user-to-run-systemctl-systemd-services-without-password