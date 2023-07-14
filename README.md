> **Deprecated** This repository is deprecated in favor of a more robust [SimpleLogin](https://simplelogin.io/) solution.


This repository contains a background process to work with a custom mail server in order to implement "masked emails".

# Overview

The background process listens on an Azure Storage Queue for commands that are then executed locally.
The commands are implemented in terms of [PowerShell scripts](https://github.com/springcomp/masked-emails-pwsh) that must be installed separately.

__Warning__: The package available publicly has a connection string to an Azure Storage account that is no longer valid. Please, make sure to update the connection string in the `/opt/masked-emails/daemon/local.settings.json` file.

# How-to Install

This package requires the following dependencies:

- [.NET 6.0 Runtime]()

```sh
sudo apt-get update && \
  sudo apt-get install -y aspnetcore-runtime-6.0
```

- [Azure Functions Core Tools v4.x]()

```sh
curl https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.gpg
sudo mv microsoft.gpg /etc/apt/trusted.gpg.d/microsoft.gpg
sudo sh -c 'echo "deb [arch=amd64] https://packages.microsoft.com/repos/microsoft-ubuntu-$(lsb_release -cs)-prod $(lsb_release -cs) main" > /etc/apt/sources.list.d/dotnetdev.list'
sudo apt-get update && \
  sudo apt-get install azure-functions-core-tools-4
```

Installing the package requires the following commands:

```sh
wget https://masked.blob.core.windows.net/debian/masked-emails-daemon.deb
dpkg -i masked-emails-daemon.deb
```

The daemon attemps to write to `syslog` using UDP.
You need to enable UDP communication in `/etc/rsyslog.conf`:

```
# provides UDP syslog reception
module(load="imudp")
input(type="imudp" port="514")
```


