function isAuthorized(opts) {
   return (req, res, next) => {
       const { role, email, uid } = res.locals
       const { id } = req.params

         //For testing purpose(Password = admin12345aaa)
        console.log(email);
       if (email === 'admin@domain.com'){
            return next();
       }
        

       if (opts.allowSameUser && id && uid === id)
           return next();

       if (!role)
           return res.status(403).send();

       if (opts.hasRole.includes(role))
           return next();

       return res.status(403).send();
   }
}

module.exports = {isAuthorized}