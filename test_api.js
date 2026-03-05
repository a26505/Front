async function test() {
    try {
        console.log("Testing Login with Dani@gmail.com...");
        let res2 = await fetch('http://localhost:5148/api/Auth/login', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                email: "Dani@gmail.com",
                password: "Daniel123"
            })
        });

        let d2 = null;
        try { d2 = await res2.json(); } catch (e) { }
        console.log("Login HTTP " + res2.status, d2);
    } catch (e) {
        console.error(e.message);
    }
}
test();
