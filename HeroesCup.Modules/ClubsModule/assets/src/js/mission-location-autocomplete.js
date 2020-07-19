$(function () {
    $(document).ready(function () {
        $(function () {
            new autoComplete({
                data: {                              // Data src [Array, Function, Async] | (REQUIRED)
                    src: async () => {
                        // User search query
                        const query = document.querySelector("#location").value;
                        // Fetch External Data Source
                        const source = await fetch('/manager/clubsmodule/bg-cities.json');
                        // Format data into JSON
                        const data = await source.json();
                        const matches = [];
                        data.find(element => {
                            if (element.name.indexOf(query) !== -1) {
                                matches.push(element.name);

                            }                           
                        });
                        console.log(matches);
                        // Return Fetched data
                        return matches;
                    },
                    key: [""],
                    cache: false
                },
                sort: (a, b) => {                    // Sort rendered results ascendingly | (Optional)
                    if (a.match < b.match) return -1;
                    if (a.match > b.match) return 1;
                    return 0;
                },
                placeHolder: "Избери населено място...",     // Place Holder text                 | (Optional)
                selector: "#location",           // Input field selector              | (Optional)
                threshold: 1,                        // Min. Chars length to start Engine | (Optional)
                debounce: 300,                       // Post duration for engine to start | (Optional)
                searchEngine: "strict",              // Search Engine type/mode           | (Optional)
                resultsList: {                       // Rendered results list object      | (Optional)
                    render: true,
                    /* if set to false, add an eventListener to the selector for event type
                       "autoComplete" to handle the result */
                    container: source => {
                        source.setAttribute("id", "towns_list");
                    },
                    destination: document.querySelector("#location"),
                    position: "afterend",
                    element: "ul"
                },
                maxResults: 5,                         // Max. number of rendered results | (Optional)
                highlight: true,                       // Highlight matching results      | (Optional)
                resultItem: {                          // Rendered result item            | (Optional)
                    content: (data, source) => {
                        source.innerHTML = data.match;
                    },
                    element: "li"
                },
                noResults: () => {                     // Action script on noResults      | (Optional)
                    const result = document.createElement("li");
                    result.setAttribute("class", "no_result");
                    result.setAttribute("tabindex", "1");
                    result.innerHTML = "No Results";
                    document.querySelector("#autoComplete_list").appendChild(result);
                },
                onSelection: feedback => {             // Action script onSelection event | (Optional)
                    document.querySelector("#location").value = feedback.selection.value;
                }
            })
        });
    });
});
