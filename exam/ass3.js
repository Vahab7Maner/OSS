var v=require('http');

var server=v.createServer(function(req,res){

	console.log("Hello from Node-JS");

});

server.listen(9000,function(){});

