var h=require('http');
var s=require('url');

var server=h.createServer(function(req,res){
 var urla=s.parse(req.url,true);
 var name=urla.query.uid;
 res.writeHead(200,{'content-type':'text/html'});
 res.write("Welcome to nodejs");
 res.end();
});

server.listen(9000,function(){
console.log("Server started");
});