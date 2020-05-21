apt-get install mariadb-server
apt-get install nginx
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
./run.sh

