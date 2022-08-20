<script>
    import Card from "./Card.svelte";
    import Button from "./Button.svelte";

    let text = '';
    let btnDisabled = true;
    let message;
    const handleInput = () => {
        if (text.trim().length <= 10){
            message = `Text must be at least 10 chars`;
            btnDisabled = true;
        }
        else {
            message = null;
            btnDisabled = false;
        }
    }
</script>

<Card>
    <h1>Kek title</h1>
    <form>
        <div class="input-group">
            <input
                type="text"
                bind:value= {text}
                placeholder="Terr as sumsing zat kips ju ehhh"
                on:input={handleInput}
            >
            <Button type='submit' disabled={btnDisabled}>
                Send
            </Button>
        </div>
        {#if message}
            {message}
        {/if}
    </form>
</Card>