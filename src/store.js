import { writable } from "svelte/store";

export const FeedbackStore = writable([
    {
        id: 1,
        rating: 3,
        text: 'loro ipso ma dude'
    },
    {
        id: 2,
        rating: 6,
        text: 'loro ipso ma dude'
    },
    {
        id: 3,
        rating: 10,
        text: 'loro ipso ma dude'
    },
]);