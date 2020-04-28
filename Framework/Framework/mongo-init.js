db.createUser(
        {
            user: "admin-test",
            pwd: "admin@test",
            roles: [
                {
                    role: "readWrite",
                    db: "demodb"
                }
            ]
        }
);