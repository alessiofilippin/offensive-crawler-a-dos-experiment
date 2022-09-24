# Offensive Crawler - A nice try to prepare a DDoS

[![Docker Image CI](https://github.com/alessiofilippin/Just-Another-Boring-Crawler/actions/workflows/docker-image.yml/badge.svg)](https://github.com/alessiofilippin/Just-Another-Boring-Crawler/actions/workflows/docker-image.yml)

This Repo contains a small Console App made in C#. I will illustrate the usage of the App in this readme. But if you want to check how I used it to simulate a DDoS in my experiment check this other repo -> [Link to Azure AKS DDoS Project](LINK)

A Docker Image is also published on the DockerHub, it could be used to execute the App in CLI mode. [Link to DockerHub](https://hub.docker.com/repository/docker/alessiofilippin/just-another-boring-crawler-cli)

## Disclaimer

This application is done for educational purpose only. I'm not responsible for any harmful usage of this App.

I developed this App in my free time! it definitly has a lot of room for improvmenets :) Be kind! ;)

## Copyright

<a rel="license" href="http://creativecommons.org/licenses/by-nc-sa/4.0/"><img alt="Creative Commons License" style="border-width:0" src="https://i.creativecommons.org/l/by-nc-sa/4.0/88x31.png" /></a><br />This work is licensed under a <a rel="license" href="http://creativecommons.org/licenses/by-nc-sa/4.0/">Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International License</a>.

<hr> 

# Usage

The App supports the following actions:

- Scrape a root url for specific depth and generates a .csv file with all the url, their loading time and DNS info (**Only in interactive mode, using the menu**).
- Bulk download the same file to attempt a bandwith starvation attack. (**Both interactive mode and CLI mode. Using the menu or calling the exe with a shell**).
- Bulk call an url with or without a proxy using multiple threads like DoS attack. (**Both interactive mode and CLI mode. Using the menu or calling the exe with a shell**).

## Scrape Root Url

This command will scrape a URL starting from a root URL. It will be possible to include/exclude certain keyworkds and specify a depth.
For each one of the URLs founds - the tool will try to calculate the loading time of the page and get some DNS informations.

This can be helpfull in finding those URLs which they take more time to load for the target website, these URLs could be good candidates for a DoS attack.

[PLACE EXAMPLE OF USAGE PICTURE]

## Bulk Download

Sometimes files are available for open download. You could take advantage of this by downloading the same file multiple times and try to saturate the target bandwith.
This command is very simple as it just starts multiple threads in parallel and attempt to download the same file multiple times.

This is probably an old fashion attack as majority of the systems will probably be protected against this.d

The files will be downloaded in the .exe directory and named with a progressive number.

[PLACE EXAMPLE OF USAGE PICTURE]

## Bulk Call

This is a useful command to perform a DoS attack. This command will create multiple threads which they will send GET request to the target.
Each thread will try to make the request unique as possible by manipulating the following headers/parameters: UserAgent, Referers, QueryParameter, Keep-Alive.

This will make harder for the target system to identify a pattern in the requests - you can also add a proxy (which it's supported by the command), if you want to change the IP or mask your connection.

> Check how I have done that by using AKS, Azure and Squid. here [Link to Azure AKS DDoS Project](LINK)

[PLACE EXAMPLE OF USAGE PICTURE]
